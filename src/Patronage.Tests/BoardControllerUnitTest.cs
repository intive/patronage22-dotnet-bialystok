using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using Patronage.Api.Controllers;
using Patronage.Api.MediatR.Board.Commands.Create;
using Patronage.Contracts.ModelDtos.Boards;
using Patronage.Models;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Patronage.DataAccess;
using Patronage.Api.MediatR.Board.Queries.GetAll;
using Patronage.Contracts.Helpers;
using System.Collections.Generic;
using Patronage.Api.MediatR.Board.Queries.GetSingle;
using Patronage.Api.MediatR.Board.Commands.Update;
using Patronage.Api.MediatR.Board.Commands.UpdateLight;
using Patronage.Contracts.ModelDtos;
using Patronage.Api.MediatR.Board.Commands.Delete;

namespace Patronage.Tests
{
    public class BoardControllerUnitTest : IClassFixture<BaseTestFixture>
    {
        private readonly BoardController _boardController;
        private readonly BaseTestFixture fixture;
        private readonly TableContext _context;
        private readonly Mock<IMediator> _mediator;

        public BoardControllerUnitTest(BaseTestFixture fixture)
        {
            this.fixture = fixture;
            _context = fixture._context;
            _mediator = new Mock<IMediator>();
            _boardController = new BoardController(_mediator.Object);
        }

        [Fact]
        public async void CreateBoardReturnsCode201()
        {
            //Arrange
            _mediator.Setup(x => x.Send(
                It.IsAny<CreateBoardCommand>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(new BoardDto() { Id = 1, Alias = "Test", Name = "TestBoard" }));
            CreateBoardCommand boardDto = new()
            {
                Data = new BoardDto()
                {
                    Id = 1,
                    Alias = "Test",
                    Name = "TestBoard"
                }
            };

            //Act
            var response = await _boardController.CreateBoard(boardDto);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<BoardDto>)!.Data;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.NotNull(dto);
            Assert.True((result as BaseResponse<BoardDto>)!.ResponseCode == StatusCodes.Status201Created);
            Assert.Null((result as BaseResponse<BoardDto>)!.BaseResponseError);
            Assert.Equal(boardDto.Data, dto);
        }
        [Fact]
        public async void GetBoardsReturnsCode400()
        {
            //Arrange
            _mediator.Setup(x => x.Send(
                It.IsAny<GetBoardsQuery>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult<PageResult<BoardDto>>(null!));

            //Act
            var response = await _boardController.GetBoards(null!);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<PageResult<BoardDto>>)!.Data;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.Null(dto);
            Assert.True((result as BaseResponse<PageResult<BoardDto>>)!.ResponseCode == StatusCodes.Status404NotFound);
        }
        
        [Fact]
        public async void GetBoardsReturnsCode200()
        {
            //Arrange
            var returnedDto = new BoardDto() { Name = "test", Alias = "test" };
            var returnedDto2 = new BoardDto() { Name = "test2", Alias = "test2" };
            List<BoardDto> boards = new() { returnedDto, returnedDto2 };
            _mediator.Setup(x => x.Send(
                It.IsAny<GetBoardsQuery>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(new PageResult<BoardDto>(boards, boards.Count, 30, 1)));

            //Act
            var response = await _boardController.GetBoards(null!);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<PageResult<BoardDto>>)!.Data;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.NotNull(dto);
            Assert.True((result as BaseResponse<PageResult<BoardDto>>)!.ResponseCode == StatusCodes.Status200OK);
            Assert.Null((result as BaseResponse<PageResult<BoardDto>>)!.BaseResponseError);
            Assert.Equal(boards, dto!.Items);
        }

        [Fact]
        public async void GetBoardByIdReturnsCode404()
        {
            //Arrange
            _mediator.Setup(x => x.Send(
                It.IsAny<GetBoardByIdQuery>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult<BoardDto>(null!));

            //Act
            var response = await _boardController.GetBoardById(1);
            var result = (response.Result as ObjectResult)!.Value!;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.True((result as BaseResponse<BoardDto>)!.ResponseCode == StatusCodes.Status404NotFound);
        }

        [Fact]
        public async void GetBoardByIdReturnsCode200()
        {
            //Arrange
            var returnedDto = new BoardDto() { Id = 1, Name = "test", Alias = "test" };
            _mediator.Setup(x => x.Send(
                It.IsAny<GetBoardByIdQuery>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(returnedDto));

            //Act
            var response = await _boardController.GetBoardById(1);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<BoardDto>)!.Data;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.NotNull(dto);
            Assert.True((result as BaseResponse<BoardDto>)!.ResponseCode == StatusCodes.Status200OK);
            Assert.Null((result as BaseResponse<BoardDto>)!.BaseResponseError);
            Assert.Equal(returnedDto, dto);
        }

        [Fact]
        public async void UpdateBoardReturnsCode404()
        {
            //Arrange
            _mediator.Setup(x => x.Send(
                It.IsAny<UpdateBoardCommand>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(false));

            //Act
            var response = await _boardController.UpdateBoard(new UpdateBoardDto() { Name = "test", Alias = "test" }, 1);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<bool>)!.Data;
            
            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.False(dto);
            Assert.True((result as BaseResponse<bool>)!.ResponseCode == StatusCodes.Status404NotFound);
        }

        [Fact]
        public async void UpdateBoardReturnsCode200()
        {
            //Arrange
            var returnedDto = new BoardDto() { Id = 1, Name = "test", Alias = "test" };
            _mediator.Setup(x => x.Send(
                It.IsAny<UpdateBoardCommand>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(true));

            //Act
            var response = await _boardController.UpdateBoard(new UpdateBoardDto() { Name = "test", Alias = "test" }, 1);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<bool>)!.Data;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.True((result as BaseResponse<bool>)!.ResponseCode == StatusCodes.Status200OK);
            Assert.Null((result as BaseResponse<bool>)!.BaseResponseError);
            Assert.True(dto);
        }

        [Fact]
        public async void UpdateBoardLightReturnsCode404()
        {
            //Arrange
            _mediator.Setup(x => x.Send(
                It.IsAny<UpdateBoardLightCommand>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(false));

            //Act
            var response = await _boardController.UpdateBoardLight(new PartialBoardDto() { Name = new PropInfo<string>() { Data = "test" }, Alias = new PropInfo<string>() { Data = "test" } }, 1);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<bool>)!.Data;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.True((result as BaseResponse<bool>)!.ResponseCode == StatusCodes.Status404NotFound);
            //Assert.NotNull((result as BaseResponse<bool>)!.BaseResponseError);
            Assert.False(dto);
        }
        
        [Fact]
        public async void UpdateBoardLightReturnsCode200()
        {
            //Arrange
            _mediator.Setup(x => x.Send(
                It.IsAny<UpdateBoardLightCommand>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(true));

            //Act
            var response = await _boardController.UpdateBoardLight(new PartialBoardDto() { Name = new PropInfo<string>() { Data = "test" }, Alias = new PropInfo<string>() { Data = "test" } }, 1);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<bool>)!.Data;
            
            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.True((result as BaseResponse<bool>)!.ResponseCode == StatusCodes.Status200OK);
            //Assert.NotNull((result as BaseResponse<bool>)!.BaseResponseError);
            Assert.True(dto);
        }

        [Fact]
        public async void DeleteBoardReturnsCode404()
        {
            //Arrange
            _mediator.Setup(x => x.Send(
                It.IsAny<DeleteBoardCommand>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(false));

            //Act
            var response = await _boardController.DeleteBoard(1);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<bool>)!.Data;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.True((result as BaseResponse<bool>)!.ResponseCode == StatusCodes.Status404NotFound);
            //Assert.NotNull((result as BaseResponse<bool>)!.BaseResponseError);
            Assert.False(dto);
        }
        
        [Fact]
        public async void DeleteBoardReturnsCode200()
        {
            //Arrange
            _mediator.Setup(x => x.Send(
                It.IsAny<DeleteBoardCommand>(),
                It.IsAny<System.Threading.CancellationToken>()
            )).Returns(Task.FromResult(true));

            //Act
            var response = await _boardController.DeleteBoard(1);
            var result = (response.Result as ObjectResult)!.Value!;
            var dto = (result as BaseResponse<bool>)!.Data;

            //Assert
            Assert.NotNull(response);
            Assert.NotNull(result);
            Assert.True((result as BaseResponse<bool>)!.ResponseCode == StatusCodes.Status200OK);
            //Assert.NotNull((result as BaseResponse<bool>)!.BaseResponseError);
            Assert.True(dto);
        }
    }
}
