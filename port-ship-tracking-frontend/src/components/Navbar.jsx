import { Link } from "react-router-dom";

function Navbar() {
  return (
    <nav>
      <Link to="/ships">Ships</Link> |{" "}
      <Link to="/ports">Ports</Link> |{" "}
      <Link to="/visits">Visits</Link> |{" "}
      <Link to="/crew">Crew</Link> |{" "}
      <Link to="/assignments">Assignments</Link>
    </nav>
  );
}

export default Navbar;