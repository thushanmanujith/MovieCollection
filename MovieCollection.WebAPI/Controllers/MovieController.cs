using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollection.Core.DTOs;
using MovieCollection.Core.Enums;
using MovieCollection.Core.Exceptions;
using MovieCollection.Movie.Domain.Infrastructure;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands;
using MovieCollection.Movie.Domain.Ports.Incoming.Commands.Results;
using MovieCollection.Movie.Domain.Ports.Incoming.Queries;
using MovieCollection.WebAPI.Authorization;
using MovieCollection.WebAPI.Exceptions;
using System.Net;
using DomainMovieDto = MovieCollection.Movie.Domain.DTOs;

namespace MovieCollection.WebAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly ICommandDispatcher _movieCommandDispatcher;
        private readonly IMovieQueries _movieQueries;

        public MovieController(IHttpContextAccessor accessor, ICommandDispatcher movieCommandDispatcher, IMovieQueries movieQueries) : base(accessor)
        {
            _movieCommandDispatcher = movieCommandDispatcher;
            _movieQueries = movieQueries;
        }

        /// <summary>
        /// Get collection for user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [RequiresUserAccess]
        [ProducesResponseType(typeof(DomainMovieDto.MovieCollectionEntityDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpGet("collection/{userId}")]
        public async Task<IActionResult> GetMoveCollectionByUser(int userId)
        {
            var moveCollection = await _movieQueries.GetMoveCollectionByUserAsync(userId);
            return Ok(moveCollection);
        }

        /// <summary>
        /// Search movies by title
        /// </summary>
        /// <param name="searchMoviesDto"></param>
        /// <returns></returns>
        [RequiresUserAccess]
        [ProducesResponseType(typeof(List<DomainMovieDto.MovieEntityDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpGet("collection/{collectionId}/search")]
        public async Task<IActionResult> SearchMoviesByTitle(int collectionId, string searchText)
        {
            var moveCollection = await _movieQueries.SearchMoviesByTitleAsync(searchText, collectionId);
            return Ok(moveCollection);
        }

        /// <summary>
        /// Admin user allow to add new movies to the system
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [RequiresAdminAccess]
        [ProducesResponseType(typeof(DomainMovieDto.MovieEntityDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpPost("add")]
        public async Task<IActionResult> AddMovie(MovieDto movieDto)
        {
            var addMovieResult = await _movieCommandDispatcher.
                Dispatch<AddMovieCommand, AddMovieResult>(new AddMovieCommand(movieDto.Title, movieDto.Description, movieDto.ThumbnailUrl, movieDto.Language));

            if (addMovieResult.MoveAlreadyExist)
                throw new ErrorCodeException(ErrorCodes.MovieAlreadyExist);

            return Ok(addMovieResult.Movie);
        }

        /// <summary>
        /// Add new collection for request user
        /// </summary>
        /// <param name="movieCollectionDto"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [RequiresUserAccess]
        [ProducesResponseType(typeof(DomainMovieDto.MovieCollectionEntityDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpPost("collection/add")]
        public async Task<IActionResult> AddCollection(MovieCollectionDto movieCollectionDto)
        {
            var addCollectionResult = await _movieCommandDispatcher.
                Dispatch<AddCollectionCommand, AddCollectionResult>(new AddCollectionCommand(movieCollectionDto.Title, GetUserId(), movieCollectionDto.MovieIds));

            if (addCollectionResult.MoveCollectionAlreadyExist)
                throw new ErrorCodeException(ErrorCodes.MoveCollectionAlreadyExist);

            return Ok(addCollectionResult.MovieCollection);
        }

        /// <summary>
        /// Add movie to the collection for request user
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [RequiresUserAccess]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpPost("collection/add_movie/{movieId}")]
        public async Task<IActionResult> AddMovieToCollection(int movieId)
        {
            var addMovieToCollectionResult = await _movieCommandDispatcher.
                Dispatch<AddMovieToCollectionCommand, AddMovieToCollectionResult>(new AddMovieToCollectionCommand(GetUserId(), movieId));

            if (addMovieToCollectionResult.CollectionNotFound)
                throw new ErrorCodeException(ErrorCodes.CollectionNotFound);

            if (addMovieToCollectionResult.MovieNotFound)
                throw new ErrorCodeException(ErrorCodes.MoveNotFound);

            return Ok(addMovieToCollectionResult.IsSuccess);
        }

        /// <summary>
        /// Remove movie from the collection the request user
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [RequiresUserAccess]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiProblem), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Forbidden)]
        [HttpDelete("collection/remove_movie/{movieId}")]
        public async Task<IActionResult> RemoveMovieFromCollection(int movieId)
        {
            var removeMovieFromCollectionResult = await _movieCommandDispatcher.
                Dispatch<RemoveMovieFromCollectionCommand, RemoveMovieFromCollectionResult>(new RemoveMovieFromCollectionCommand(GetUserId(), movieId));

            if (removeMovieFromCollectionResult.CollectionNotFound)
                throw new ErrorCodeException(ErrorCodes.CollectionNotFound);

            if (removeMovieFromCollectionResult.MoveNotFound)
                throw new ErrorCodeException(ErrorCodes.MoveNotFound);

            return Ok(removeMovieFromCollectionResult.IsSuccess);
        }
    }
}
