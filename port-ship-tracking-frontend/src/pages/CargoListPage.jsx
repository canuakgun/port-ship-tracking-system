import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { getCargoesByShipId } from "../api/cargoApi";

function CargoListPage() {
  const { shipId } = useParams();
  const [cargoes, setCargoes] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getCargoesByShipId(shipId)
      .then((response) => {
        setCargoes(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching cargoes:", error);
        setLoading(false);
      });
  }, [shipId]);

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Cargoes for Ship #{shipId}</h1>
      <table>
        <thead>
          <tr>
            <th>Description</th>
            <th>Weight (Ton)</th>
            <th>Type</th>
          </tr>
        </thead>
        <tbody>
          {cargoes.map((cargo) => (
            <tr key={cargo.cargoId}>
              <td>{cargo.description}</td>
              <td>{cargo.weightTon}</td>
              <td>{cargo.cargoType}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default CargoListPage;