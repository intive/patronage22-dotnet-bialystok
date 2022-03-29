using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.Extensions.DependencyInjection;
using Patronage.Common;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;
using Patronage.Contracts.ModelDtos.Boards;
using Patronage.Contracts.ModelDtos.Issues;
using Patronage.Contracts.ModelDtos.Projects;
using Patronage.Models;
using LuceneDirectory = Lucene.Net.Store.Directory;

namespace Patronage.DataAccess.Services
{
    public class LuceneService : ILuceneService, IDisposable
    {
        private static class FieldNames
        {
            public const string BoardName = "boardName";
            public const string IssueName = "issueName";
            public const string IssueDescription = "issueDescription";
            public const string ProjectName = "projectName";
        }

        private const LuceneVersion AppLuceneVersion = LuceneVersion.LUCENE_48;
        private readonly IndexWriter writer;
        private readonly LuceneDirectory dir;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TableContext _tableContext;

        public LuceneService(IServiceScopeFactory scopeFactory, TableContext tableContext)
        {
            _scopeFactory = scopeFactory;
            _tableContext = tableContext;

            string indexName = "index";
            string indexPath = Path.Combine(Environment.CurrentDirectory, indexName);

            dir = FSDirectory.Open(indexPath);

            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);

            writer = new IndexWriter(dir, indexConfig);
        }

        public void AddDocument(IEntity entity)
        {
            var doc = new Document();

            var type = entity.GetType();
            if (type.Name.Contains("Board"))
            {
                doc.Add(new TextField(FieldNames.BoardName, entity.Name, Field.Store.YES));
                writer.AddDocument(doc);
            }
            if (type.Name.Contains("Project"))
            {
                doc.Add(new TextField(FieldNames.ProjectName, entity.Name, Field.Store.YES));

                writer.AddDocument(doc);
            }
            if (type.Name.Contains("Issue"))
            {
                doc.Add(new TextField(FieldNames.IssueName, entity.Name, Field.Store.YES));

                if (!String.IsNullOrEmpty(entity.Description))
                {
                    doc.Add(new TextField(FieldNames.IssueDescription, entity.Description, Field.Store.YES));
                }

                writer.AddDocument(doc);
            }

            writer.Commit();
        }

        public void DeleteDocument(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public FilteredEntities Search(string name, string description)
        {
            var top = SearchDocuments(FieldNames.BoardName, name);

            var boardsList = CreateList(top, FieldNames.BoardName,
                (x, resultName) => x.Boards.FirstOrDefault(z => z.Name.ToLower() == resultName.ToLower()))
                .Select(x => new BaseBoardDto(x!));

            top = SearchDocuments(FieldNames.ProjectName, name);

            var projectList = CreateList(top, FieldNames.ProjectName,
                (x, resultName) => x.Projects.FirstOrDefault(z => z.Name.ToLower() == resultName.ToLower()))
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

            top = SearchDocuments(FieldNames.IssueName, name);

            var issueName = CreateList(top, FieldNames.IssueName,
                (x, resultName) => x.Issues.FirstOrDefault(z => z.Name.ToLower() == resultName.ToLower()));

            top = SearchDocuments(FieldNames.IssueDescription, name);

            var issueDescription = CreateList(top, FieldNames.IssueDescription,
                (x, resultName) => x.Issues.FirstOrDefault(z => z.Description!.ToLower() == resultName.ToLower()));

            //issueName.Append(issueDescription);

            var issueList = issueName.Distinct().Select(x => new BaseIssueDto(x!));

            return new FilteredEntities
            {
                Boards = boardsList,
                Issues = issueList,
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

        public void Dispose()
        {
            writer.Dispose();
            dir.Dispose();
        }
    }
}