using AutoMapper;
using NanoCell.Application.CRMTuyenDoc.Commands;
using NanoCell.Application.TodoItems.Commands.CreateTodoItem;
using NanoCell.Application.UnitTests.Common;
using NanoCell.Infrastructure.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NanoCell.Application.UnitTests.CRMTuyenDoc.Commands
{
    [Collection("QueryTests")]
    public class CreateCRMTuyenDocCommandTests 
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCRMTuyenDocCommandTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task Handle_ShouldPersistCRMTuyenDoc()
        {
            var command = new CreateCRMTuyenDocCommand
            {
                Code="abcaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaas",
                Name="Tuyen 1"
            };

            var handler = new CreateCRMTuyenDocCommand.CreateCRMTuyenDocCommandHandler(_context,_mapper);

            var result = await handler.Handle(command, CancellationToken.None);

            var entity = _context.CRMDMTuyenDocs.Find(result);
           

            entity.ShouldNotBeNull();
            entity.Code.ShouldBe(command.Code);
        }
    }
}
