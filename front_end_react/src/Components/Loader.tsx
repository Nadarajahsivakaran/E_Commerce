import { Spinner } from "react-bootstrap";

const Loader = () => {
  return (
    <Spinner
      animation="border"
      role="status"
      className="d-block mx-auto my-5"
    />
  );
};

export default Loader;
