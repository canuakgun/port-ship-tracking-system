import { useEffect, useState } from "react";
import { getShips } from "../api/shipApi";

function ShipListPage() {
  const [ships, setShips] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getShips()
      .then((response) => {
        setShips(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching ships:", error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Ships</h1>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>IMO</th>
            <th>Type</th>
            <th>Flag</th>
            <th>Year Built</th>
          </tr>
        </thead>
        <tbody>
          {ships.map((ship) => (
            <tr key={ship.shipId}>
              <td>{ship.name}</td>
              <td>{ship.imo}</td>
              <td>{ship.type}</td>
              <td>{ship.flag}</td>
              <td>{ship.yearBuilt}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ShipListPage;