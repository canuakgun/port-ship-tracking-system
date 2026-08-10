import { Link, useNavigate } from "react-router-dom";

function Navbar() {
  const navigate = useNavigate();
  const username = localStorage.getItem("username");

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    localStorage.removeItem("role");
    navigate("/login");
  };

  return (
    <nav>
      <Link to="/ships">Ships</Link> |{" "}
      <Link to="/ports">Ports</Link> |{" "}
      <Link to="/visits">Visits</Link> |{" "}
      <Link to="/crew">Crew</Link> |{" "}
      <Link to="/assignments">Assignments</Link>
      {username && (
        <span style={{ float: "right" }}>
          {username} <button onClick={handleLogout} type="button">Logout</button>
        </span>
      )}
    </nav>
  );
}

export default Navbar;