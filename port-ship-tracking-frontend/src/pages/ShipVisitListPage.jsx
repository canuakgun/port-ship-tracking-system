import { useEffect, useState } from "react";
import { getVisits } from "../api/shipVisitApi";

function ShipVisitListPage() {
  const [visits, setVisits] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getVisits()
      .then((response) => {
        setVisits(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching visits:", error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Ship Visits</h1>
      <table>
        <thead>
          <tr>
            <th>Ship</th>
            <th>Port</th>
            <th>Arrival</th>
            <th>Departure</th>
            <th>Purpose</th>
          </tr>
        </thead>
        <tbody>
          {visits.map((visit) => (
            <tr key={visit.visitId}>
              <td>{visit.shipName}</td>
              <td>{visit.portName}</td>
              <td>{new Date(visit.arrivalDate).toLocaleDateString()}</td>
              <td>{new Date(visit.departureDate).toLocaleDateString()}</td>
              <td>{visit.purpose}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ShipVisitListPage;