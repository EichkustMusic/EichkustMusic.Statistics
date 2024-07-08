using AutoMapper;
using EichkustMusic.States.Application.UnitOfWork;
using EichkustMusic.States.Domain.Entities;
using EichkustMusic.Statistics.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EichkustMusic.Statistics.API.Controllers
{
    #region ApiV1
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ListeningsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListeningsController(
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
            var listening = await _unitOfWork.ListeningRepository
                .GetFromStatisticsById(id);

            if (listening is null)
            {
                return NotFound(nameof(id));
            }

            var listeningDto = _mapper.Map<SimpleStatisticsEntityDto>(listening);

            return Ok(listeningDto);
        }

        [HttpPost]
        public async Task<ActionResult<SimpleStatisticsEntityDto>> AddToStatistic(
            SimpleStatisticsEntityForCreateDto listeningForCreateDto)
        {
            var listening = _mapper.Map<Listening>(listeningForCreateDto);

            _unitOfWork.ListeningRepository.AddToStatistics(listening);

            await _unitOfWork.SaveChangesAsync();

            var createdListeningDto = _mapper.Map<SimpleStatisticsEntityDto>(listening);

            return CreatedAtAction(nameof(GetFromStatistics), new
            {
                listening.Id,
            },
            createdListeningDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromStatistics(int id)
        {
            var listening = await _unitOfWork.ListeningRepository
                .GetFromStatisticsById(id);

            if (listening is null)
            {
                return NotFound(nameof(id));
            }

            _unitOfWork.ListeningRepository
                .RemoveFromStatistics(listening);

            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("for_year/{year}/for_track/{trackId}")]
        public async Task<ActionResult<IEnumerable<StatisticsByDateItem>>> GetStatisticsForYearByMonthForTrack(
            int year, int trackId)
        {
            var statistics = await _unitOfWork.ListeningRepository
                .GetStatisticsByMonthsForYearAsync(year, trackId);

            return Ok(statistics);
        }

        [HttpGet("for_year/{year}/for_month/{month}/for_track/{trackId}")]
        public async Task<ActionResult<IEnumerable<StatisticsByDateItem>>> GetStatisticsForMonthByDaysForTrack(
            int year, int month, int trackId)
        {
            var statistics = await _unitOfWork.ListeningRepository
                .GetStatisticsByDaysForMonthAsync(year, month, trackId);

            return Ok(statistics);
        }
    }
    #endregion
}
