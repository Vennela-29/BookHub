import {useState} from 'react';
import {useNavigate} from 'react-router-dom';
import { Routes,Route } from 'react-router-dom';
import Login from './Login';
import Students from './Student/Students';
import AdminDashboard from './Admin/AdminDashboard';

function App() {
  const navigate=useNavigate();
  const[username,setUsername]=useState('');
  const[password,setPassword]=useState("");
  const[Error,setError]=useState('');
  const[loading,setLoading]=useState('');
  const handleLogin= async (e)=>{
    e.preventDefault();
    setError("");
    setLoading("Loading");
    const response= await fetch("http://localhost:5000/api/auth/Login",{
      method:'POST',
      headers:
      {'Content-Type':'application/json'},
      body: JSON.stringify({username,password})
    });
    if(response.ok){
      setLoading("");
      const data=await response.json();
      localStorage.setItem("token", data.token);
      localStorage.setItem("role", data.role);
      localStorage.setItem("username", data.username);
      if(data.role==="Admin"){
        navigate('./Admin/AdminDashboard');
      }
      else{
      navigate('./Student/Students');
      }
      setUsername('');
      setPassword('');
    }
    else{
      setLoading("");
      const err=await response.json();
      setError(err.message);
    }
  }


  return (
    <div className="App">
      <Routes>
       <Route path="/" element={<Login 
          username={username}
          password={password}
          setUsername={setUsername}
          setPassword={setPassword}
          handleLogin={handleLogin}
          Error={Error}
          loading={loading}
      />} />
      <Route path="/Student/Students" element={<Students />} />
      <Route path="/Admin/AdminDashboard" element={<AdminDashboard/>} />
      {/* <Route path='/Logout' element={handleLogout}/> */}
      {/* <Route path="/Profile" element={<Profile />} />
      <Route path="/Borrow" element={<Borrow />} />
      <Route path="/Return" element={<Return />} />
      <Route path="/ViewBooks" element={<ViewBooks />} />
      <Route path="/Search" element={<Search />} /> */}
      </Routes>
    </div>
  );
}

export default App;
