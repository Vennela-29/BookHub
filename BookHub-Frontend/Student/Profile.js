import React, { useState, useEffect } from "react";

const Profile = () => {
  const [student, setStudent] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem("token");

    const fetchStudent = async () => {
      try {
        const response = await fetch(
          "http://localhost:5000/api/Student/ViewYourProfile",
          {
            method: "GET",
            headers: {
              Authorization: `Bearer ${token}`,
            },
          }
        );

        if (response.ok) {
          const data = await response.json();
          console.log("Profile response:", data);
          setStudent(data);
          localStorage.setItem("studentId", data.id);
          localStorage.setItem("Books",JSON.stringify(data.borrowedList));
          // console.log("Data"+data.borrowedList);
        } else {
          console.error("Failed to fetch profile:"+response.status);
        }
      } catch (err) {
        console.error("Error fetching profile:"+err);
      } finally {
        setLoading(false);
      }
    };

    fetchStudent();
  }, []);

  if (loading) {
    return (
      <div className="Profile">
        <p>Loading student profile...</p>
      </div>
    );
  }

  if (!student) {
    return (
      <div className="Profile">
        <p>Student profile not found.</p>
      </div>
    );
  }

  return (
    <div className="Profile">
      <div className="profile-title">
        <h2> STUDENT PROFILE</h2>
        <div className="profile-data">
          {Object.entries(student).map(([key, value]) => (
            <div className="profile-field" key={key}>
              <strong>{key.replace(/([A-Z])/g, " $1").toUpperCase()}:</strong>{" "}
              <span style={{ whiteSpace: "pre-line" }}>{Array.isArray(value) ? value.join("\n") : value}</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default Profile;
