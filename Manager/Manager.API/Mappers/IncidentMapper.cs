using Manager.API.Dtos.Incident;
using Manager.API.Models;

namespace Manager.API.Mappers
{
    public static class IncidentMapper
    {
        public static IncidentDto ToIncidentDto(this Incident incident)
        {
            return new IncidentDto
            {
                Id = incident.Id,
                BookingId = incident.BookingId,
                UserId = incident.UserId,
                Title = incident.Title,
                Description = incident.Description,
                Status = incident.Status,
                CreateAt = incident.CreateAt
            };
        }

        public static Incident ToIncident(this CreateIncidentDto dto)
        {
            return new Incident
            {
                BookingId = dto.BookingId,
                UserId = dto.UserId,
                Title = dto.Title,
                Description = dto.Description
            };
        }
    }
}