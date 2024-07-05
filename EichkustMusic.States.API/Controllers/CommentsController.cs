using AutoMapper;
using EichkustMusic.States.Application.UnitOfWork;
using EichkustMusic.States.Domain.Entities;
using EichkustMusic.Statistics.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EichkustMusic.Statistics.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentsController(
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));

            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet("for_track/{trackId}")]
        public async Task<ActionResult<CommentDto>> ListTracksComments(int trackId)
        {
            var comments = await _unitOfWork.CommentRepository
                .ListTracksComments(trackId);

            var commentDtos = new List<CommentDto>();

            comments.ForEach(c => commentDtos.Add(
                _mapper.Map<CommentDto>(c)));

            return Ok(commentDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetFromStatistics(
            int id)
        {
            var comment = await _unitOfWork.CommentRepository
                .GetFromStatisticsById(id);

            if (comment is null)
            {
                return NotFound(nameof(id));
            }

            var commentDto = _mapper.Map<CommentDto>(comment);

            return Ok(commentDto);
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> AddToStatistic(
            CommentForCreateDto commentForCreateDto)
        {
            var comment = _mapper.Map<Comment>(commentForCreateDto);

            _unitOfWork.CommentRepository.AddToStatistics(comment);

            await _unitOfWork.SaveChangesAsync();

            var createdCommentDto = _mapper.Map<CommentDto>(comment);

            return CreatedAtAction(nameof(GetFromStatistics), new
            {
                comment.Id,
            },
            createdCommentDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromStatistics(int id)
        {
            var comment = await _unitOfWork.CommentRepository
                .GetFromStatisticsById(id);

            if (comment is null)
            {
                return NotFound(nameof(id));
            }

            _unitOfWork.CommentRepository
                .RemoveFromStatistics(comment);

            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("for_year/{year}/for_track/{trackId}")]
        public async Task<ActionResult<IEnumerable<StatisticsByDateItem>>> GetStatisticsForYearByMonthForTrack(
            int year, int trackId)
        {
            var statistics = await _unitOfWork.CommentRepository
                .GetStatisticsByMonthsForYearAsync(year, trackId);

            return Ok(statistics);
        }

        [HttpGet("for_year/{year}/for_month/{month}/for_track/{trackId}")]
        public async Task<ActionResult<IEnumerable<StatisticsByDateItem>>> GetStatisticsForMonthByDaysForTrack(
            int year, int month, int trackId)
        {
            var statistics = await _unitOfWork.CommentRepository
                .GetStatisticsByDaysForMonthAsync(year, month, trackId);

            return Ok(statistics);
        }
    }
}
