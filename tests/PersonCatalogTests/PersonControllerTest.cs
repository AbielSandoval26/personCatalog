using Application.Commands;
using Application.Queries;
using Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using personCatalogAPI.Controllers;
using PersonCatalogTests.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonCatalogTests
{
    public class PersonControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly PersonsController _controller;

        public PersonControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new PersonsController(_mediatorMock.Object);
        }

        [Fact]
        public async Task GetAllPersons()
        {
            var persons = new List<Person> 
            { 
                new Person { Id = 1, Nombre = "Abiel", Apellido = "Sandoval", Email = "asandoval@gmail.com", Direccion = "C/ Prueba #1", Telefono = "8090000000" }, 
                new Person { Id = 3, Nombre = "Georgina", Apellido = "Martinez", Email = "gmartinez@gmail.com", Direccion = "C/ Prueba #3", Telefono = "8091236547" } 
            };


            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPersonsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(persons);
            

            var result = await _controller.GetAll();


            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(persons);
        }

        [Fact]
        public async Task GetPersonByID_ReturnsOkResult_WhenPersonExists()
        {
            var person = new Person { Id = 1, Nombre = "Abiel", Apellido = "Sandoval", Email = "asandoval@gmail.com", Direccion = "C/ Prueba #1", Telefono = "8090000000" };
            
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(person);

            var result = await _controller.GetPersonByID(1);

            //result.Should().BeOfType<NotFoundResult>();
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Person>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetPersonByID_ReturnsNotFound_WhenPersonDoesNotExist()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonByIdQuery>(), default))
                 .ReturnsAsync((Person)null);

            var result = await _controller.GetPersonByID(1);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task CreatePerson_ReturnsOkResult_WithPersonId()
        {
            var newPersonId = 4;
            var command = new CreatePersonCommand { Nombre = "Eno", Apellido = "Sandoval", Email = "esandoval@gmail.com", Direccion = "C/ Prueba #9", Telefono = "8490000000" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePersonCommand>(), default)).ReturnsAsync(newPersonId);

            var result = await _controller.Create(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<int>(okResult.Value);
            Assert.Equal(4, returnValue);

        }

        [Fact]
        public async Task UpdatePerson_ShouldReturnNoContent_WnenPersonIsUpdated()
        {
            var command = new UpdatePersonCommand { Id = 1, Nombre = "Abiel", Apellido = "Sandoval", Email = "ansandoval@gmail.com", Direccion = "C/ Prueba #9", Telefono = "8490001400" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePersonCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var result = await _controller.UpdatePerson(command.Id, command);

            result.Should().BeOfType<NoContentResult>();

        }

        [Fact]
        public async Task UpdatePerson_ShouldReturnBadRequest_WhenIdsDoNotMatch()
        {
            var command = new UpdatePersonCommand { Id = 1, Nombre = "Abiel", Apellido = "Sandoval", Email = "ansandoval@gmail.com", Direccion = "C/ Prueba #9", Telefono = "8490001400" };

            var result = await _controller.UpdatePerson(2, command);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task UpdatePerson_ShouldReturnNotFound_WhenPersonDoesNotExist()
        {
            var command = new UpdatePersonCommand { Id = 1, Nombre = "Abiel", Apellido = "Sandoval", Email = "ansandoval@gmail.com", Direccion = "C/ Prueba #9", Telefono = "8490001400" };
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdatePersonCommand>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync(0);

            var result = await _controller.UpdatePerson(1, command);

            result?.Should().BeOfType<NotFoundResult>();

        }

        [Fact]
        public async Task DeletePerson_ShouldReturnNoContent_WhenPersonIsDeleted()
        {
            var personId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePersonCommand>(), default)).ReturnsAsync(personId);

            var result = await _controller.DeletePerson(personId);

            result.Should().BeOfType<NoContentResult>();
            
        }

        [Fact]
        public async Task DeletePerson_ShouldReturnNotFound_WhenPersonDoesNotExist()
        {
            var personId = 1;
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeletePersonCommand>(), default))
                .ReturnsAsync(0);

            var result = await _controller.DeletePerson(personId);

            result?.Should().BeOfType<NotFoundResult>();

        }
    }
}
