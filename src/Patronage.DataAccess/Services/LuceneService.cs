using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Patronage.Common;
using Patronage.Contracts.Helpers;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Contracts.ModelDtos.Boards;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.Models;
using System.Reflection;
using System.Text;
using LuceneDirectory = Lucene.Net.Store.Directory;

namespace Patronage.DataAccess.Services
{
    public class LuceneService : ILuceneService, IDisposable
    {
        private const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
        private readonly IndexWriter writer;
        private readonly LuceneDirectory dir;
        private readonly TableContext _tableContext;

        public LuceneService(TableContext tableContext)
        {
            _tableContext = tableContext;

            string indexName = LuceneFieldNames.IndexName;
            string indexPath = Path.Combine(Environment.CurrentDirectory, indexName);

            dir = FSDirectory.Open(indexPath);

            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);

            writer = new IndexWriter(dir, indexConfig);
        }

        public void AddDocument(IEntity entity)
        {
            var doc = new Document();

            var list = entity.GetLuceneTextField().ToList();

            doc.Add(list.First());

            if (list.Count > 1)
            {
                doc.Add(list.Last());
            }

            writer.AddDocument(doc);

            writer.Commit();
        }

        public void UpdateDocument(IEntity entity, int id)
        {
            var type = entity.GetType();

            IEntity? oldEntity = null;

            if (type.Name.Contains("Board"))
            {
                oldEntity = _tableContext.Boards
                    .Where(x => x.Id == id)
                    .Select(x => new BaseBoardDto(x))
                    .FirstOrDefault();
            }

            if (type.Name.Contains("Project"))
            {
                oldEntity = _tableContext.Projects
                    .Where(x => x.Id == id)
                    .Select(x => new ProjectDto
                    {
                        Id = x!.Id,
                        Name = x.Name,
                        Alias = x.Alias,
                        CreatedOn = x.CreatedOn,
                        Description = x.Description,
                        IsActive = x.IsActive,
                        ModifiedOn = x.ModifiedOn
                    }).FirstOrDefault();
            }

            if (type.Name.Contains("Issue"))
            {
                oldEntity = _tableContext.Issues
                    .Where(x => x.Id == id)
                    .Select(x => new BaseIssueDto(x))
                    .FirstOrDefault();
            }

            var terms = oldEntity!.GetLuceneTerm();

            if (!oldEntity.Name.Equals(entity.Name))
            {
                writer.DeleteDocuments(terms.First());
            }

            if (terms.ToList().Count > 1)
            {
                if (type.Name.Contains("Issue") && (entity.Description?.Equals(oldEntity.Description) ?? false))
                {
                    writer.DeleteDocuments(terms.Last());
                }
            }

            AddDocument(entity);
        }

        public FilteredEntities Search(string? name = null, string? description = null)
        {
            IEnumerable<BaseBoardDto>? boardsList = null;
            IEnumerable<ProjectDto>? projectList = null;
            IEnumerable<Issue?>? issueName = null;
            IEnumerable<Issue?>? issueDescription = null;
            var issueList = new List<BaseIssueDto>();

            if (name is not null)
            {
                var topDoc = SearchDocuments(LuceneFieldNames.BoardName, name);

                boardsList = CreateList(topDoc, LuceneFieldNames.BoardName,
                    (x, resultName) => x.Boards.FirstOrDefault(z => z.Name.Equals(resultName)))
                    .Select(x => new BaseBoardDto(x!));

                topDoc = SearchDocuments(LuceneFieldNames.ProjectName, name);

                projectList = CreateList(topDoc, LuceneFieldNames.ProjectName,
                    (x, resultName) => x.Projects.FirstOrDefault(z => z.Name.Equals(resultName)))
                    .Select(x => new ProjectDto
                    {
                        Id = x!.Id,
                        Name = x.Name,
                        Alias = x.Alias,
                        CreatedOn = x.CreatedOn,
                        Description = x.Description,
                        IsActive = x.IsActive,
                        ModifiedOn = x.ModifiedOn
                    });

                topDoc = SearchDocuments(LuceneFieldNames.IssueName, name);

                issueName = CreateList(topDoc, LuceneFieldNames.IssueName,
                    (x, resultName) => x.Issues.FirstOrDefault(z => z.Name.Equals(resultName)));

                issueList.AddRange(issueName.Select(x => new BaseIssueDto(x!)));
            }
            if (description is not null)
            {
                var top = SearchDocuments(LuceneFieldNames.IssueDescription, description);

                issueDescription = CreateList(top, LuceneFieldNames.IssueDescription,
                    (x, resultName) => x.Issues.FirstOrDefault(z => z.Description == null ? false : z.Description.Equals(resultName)));

                issueList.AddRange(issueDescription.Select(x => new BaseIssueDto(x!)));
            }

            var test = issueList.Distinct().ToArray();

            return new FilteredEntities
            {
                Boards = boardsList,
                Issues = issueList.Distinct(),
                Projects = projectList
            };
        }

        private TopDocs SearchDocuments(string field, string name, int n = 20)
        {
            using DirectoryReader reader = writer.GetReader(applyAllDeletes: true);

            var searcher = new IndexSearcher(reader);

            QueryParser parser = new QueryParser(AppLuceneVersion, field, writer.Analyzer);
            Query query = parser.Parse(name);
            return searcher.Search(query, n: n);
        }

        private IEnumerable<T> CreateList<T>(TopDocs topDocs, string fieldName, Func<TableContext, string, T> func)
        {
            using DirectoryReader reader = writer.GetReader(applyAllDeletes: true);

            var searcher = new IndexSearcher(reader);

            for (int i = 0; i < topDocs.TotalHits; i++)
            {
                var resultDoc = searcher.Doc(topDocs.ScoreDocs[i].Doc);

                var entityName = resultDoc.Get(fieldName);

                var entity = func(_tableContext, entityName);

                if (entity is not null)
                {
                    yield return entity;
                }
            }
        }

        public void DeleteDocument(IEntity entity)
        {
            var terms = entity.GetLuceneTerm();

            writer.DeleteDocuments(entity.GetLuceneTerm().First());

            if (terms.ToList().Count > 1)
            {
                writer.DeleteDocuments(entity.GetLuceneTerm().Last());
            }
        }

        public void Dispose()
        {
            writer.Dispose();
            dir.Dispose();
        }
    }
}