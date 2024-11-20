using backend.Models;

namespace backend.Interfaces
{
    public interface IFlightRepo
    {
        ICollection<Flight> GetFlights();
        Flight Flight(int id);
        ICollection<Flight> getflighthome();
        ICollection<Flight> getFlightdeparted();
        ICollection<Flight> getFlightArrived();
        bool FlightExists(int id);  
        bool CreateFlight(Flight flight);
        ICollection<Flight> GEtFlightByPilotID(int id);
        ICollection<Flight> GEtFlightByCoPilotID(int id);
        ICollection<Flight> GEtFlightByAirID(int id);
        ICollection<Flight> GetFlightByPilotIDDeparted(int id);
        ICollection<Flight> GetFlightByCoPilotIDDeparted(int id);
        ICollection<Flight> GetFlightByPilotIArrived(int id);
        ICollection<Flight> GetFlightByCoPilotIArrived(int id);
        ICollection<Flight> GetcabinDeparted(string group);
        ICollection<Flight> GetcabinArrived(string group);
        ICollection<Flight> GettechncianArrived(string group);
        ICollection<Flight> GettechncianDeparted(string group);
        ICollection<Flight> GetFlightByCabinGroup(string group);
        ICollection<Flight> GetFlightByCabinGroupD(string group);

        ICollection<Flight> GetFlightbyTechnicanGroup(string  group);
        ICollection<Flight> GetFlightbyTechnicanGroupD(string  group);

        bool UpdateFlight(Flight flight);
        bool DeleteFlight(int id);
        bool Save();
        bool DeleteFlight(Flight flight);
    }
}
