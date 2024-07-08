using AutoMapper;
using EichkustMusic.States.Application.UnitOfWork;
using EichkustMusic.States.Domain.Entities;
using EichkustMusic.Statistics.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EichkustMusic.Statistics.API.Controllers
{
    #region ApiV1
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LikesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LikesController(
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));

            _unitOfWork = unitOfWork
                ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SimpleStatisticsEntityDto>> GetFromStatistics(
            int id)
        {
            var like = await _unitOfWork.LikeRepository
                .GetFromStatisticsById(id);

            if (like is null)
            {
                return NotFound(nameof(id));
            }

            var likeDto = _mapper.Map<SimpleStatisticsEntityDto>(like);

            return Ok(likeDto);
        }

        [HttpPost]
        public async Task<ActionResult<SimpleStatisticsEntityDto>> AddToStatistic(
            SimpleStatisticsEntityForCreateDto likeForCreateDto)
        {
            var like = _mapper.Map<Like>(likeForCreateDto);

            _unitOfWork.LikeRepository.AddToStatistics(like);

            await _unitOfWork.SaveChangesAsync();

            var createdLikeDto = _mapper.Map<SimpleStatisticsEntityDto>(like);

            return CreatedAtAction(nameof(GetFromStatistics), new
            {
                like.Id,
            },
            createdLikeDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromStatistics(int id)
        {
            var like = await _unitOfWork.LikeRepository
                .GetFromStatisticsById(id);

            if (like is null)
            {
                return NotFound(nameof(id));
            }

            _unitOfWork.LikeRepository
                .RemoveFromStatistics(like);

            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("for_year/{year}/for_track/{trackId}")]
        public async Task<ActionResult<IEnumerable<StatisticsByDateItem>>> GetStatisticsForYearByMonthForTrack(
            int year, int trackId)
        {
            var statistics = await _unitOfWork.LikeRepository
                .GetStatisticsByMonthsForYearAsync(year, trackId);

            return Ok(statistics);
        }

        [HttpGet("for_year/{year}/for_month/{month}/for_track/{trackId}")]
        public async Task<ActionResult<IEnumerable<StatisticsByDateItem>>> GetStatisticsForMonthByDaysForTrack(
            int year, int month, int trackId)
        {
            var statistics = await _unitOfWork.LikeRepository
                .GetStatisticsByDaysForMonthAsync(year, month, trackId);

            return Ok(statistics);
        }
    }
    #endregion
}
