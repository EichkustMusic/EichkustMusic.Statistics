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
    public class SharingsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SharingsController(
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
            var sharing = await _unitOfWork.SharingRepository
                .GetFromStatisticsById(id);

            if (sharing is null)
            {
                return NotFound(nameof(id));
            }

            var sharingDto = _mapper.Map<SimpleStatisticsEntityDto>(sharing);

            return Ok(sharingDto);
        }

        [HttpPost]
        public async Task<ActionResult<SimpleStatisticsEntityDto>> AddToStatistic(
            SimpleStatisticsEntityForCreateDto sharingForCreateDto)
        {
            var sharing = _mapper.Map<Sharing>(sharingForCreateDto);

            _unitOfWork.SharingRepository.AddToStatistics(sharing);

            await _unitOfWork.SaveChangesAsync();

            var createdSharingDto = _mapper.Map<SimpleStatisticsEntityDto>(sharing);

            return CreatedAtAction(nameof(GetFromStatistics), new
            {
                sharing.Id,
            },
            createdSharingDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromStatistics(int id)
        {
            var sharing = await _unitOfWork.SharingRepository
                .GetFromStatisticsById(id);

            if (sharing is null)
            {
                return NotFound(nameof(id));
            }

            _unitOfWork.SharingRepository
                .RemoveFromStatistics(sharing);

            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("for_year/{year}/for_track/{trackId}")]
        public async Task<ActionResult<IEnumerable<StatisticsByDateItem>>> GetStatisticsForYearByMonthForTrack(
            int year, int trackId)
        {
            var statistics = await _unitOfWork.SharingRepository
                .GetStatisticsByMonthsForYearAsync(year, trackId);

            return Ok(statistics);
        }

        [HttpGet("for_year/{year}/for_month/{month}/for_track/{trackId}")]
        public async Task<ActionResult<IEnumerable<StatisticsByDateItem>>> GetStatisticsForMonthByDaysForTrack(
            int year, int month, int trackId)
        {
            var statistics = await _unitOfWork.SharingRepository
                .GetStatisticsByDaysForMonthAsync(year, month, trackId);

            return Ok(statistics);
        }
    }
}
