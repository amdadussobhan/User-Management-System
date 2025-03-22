import React, { useState, useEffect } from "react";
import axios from "axios";
import { Table, Spinner, Alert } from "react-bootstrap";

const UserList = () => {
  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    axios
      .get("http://localhost:5166/api/user/list")
      .then((response) => {
        setUsers(response.data);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <Spinner animation="border" role="status"></Spinner>;
  }

  if (error) {
    return <Alert variant="danger">Error fetching users: {error}</Alert>;
  }

  return (
    <div className="container mt-4">
      <h1>User List</h1>
      <Table striped bordered hover responsive>
        <thead className="thead-dark">
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Email</th>
            <th>Last Login</th>
          </tr>
        </thead>
        <tbody>
          {users.map((user) => (
            <tr key={user.id}>
              <td>{user.id}</td>
              <td>{user.name}</td>
              <td>{user.email}</td>
              <td>
                {user.lastLogin
                  ? new Date(user.lastLogin).toLocaleString()
                  : "Never"}
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
};

export default UserList;
