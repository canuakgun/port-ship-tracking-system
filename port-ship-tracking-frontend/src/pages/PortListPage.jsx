import { useEffect, useState } from "react";
import { getPorts } from "../api/portApi";

function PortListPage() {
  const [ports, setPorts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getPorts()
      .then((response) => {
        setPorts(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching ports:", error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Loading...</p>;

  return (
    <div>
      <h1>Ports</h1>
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Country</th>
            <th>City</th>
          </tr>
        </thead>
        <tbody>
          {ports.map((port) => (
            <tr key={port.portId}>
              <td>{port.name}</td>
              <td>{port.country}</td>
              <td>{port.city}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default PortListPage;