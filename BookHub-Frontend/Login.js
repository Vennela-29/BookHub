import React from 'react'
import WinWireLogo from './WinWireLogo.png';

const Login = ({username,password,setUsername,setPassword,handleLogin,Error,loading}) => {

  return (
    <div className='container'>
    <img src={WinWireLogo} alt='Winwire Logo'/>
      <h2 id="title">LIBRARY MANAGEMENT SYSTEM</h2>
        <form className='Login' onSubmit={handleLogin}>
          <div id="form-title">
            <h2>LOGIN</h2>
          </div>
          <div className='username'>
        <label htmlFor='Login'>USERNAME </label>
        <input type='text'
        autoFocus
        id='username'
        placeholder='Enter Email here'
        required
        value={username}
        onChange={(e)=>setUsername(e.target.value)}
        autoComplete="username"
        />
        </div>
        <div className='password'>
        <label >PASSWORD </label>
        <input type='password' 
        id='password'
        placeholder='password'
        required
        value={password}
        onChange={(e)=>setPassword(e.target.value)}
        autoComplete="current-password"
        />
        </div>
        <button id='login' type='submit'>Login</button>
        {loading &&
        <p>Please wait...</p>
        }
        <p className={`message ${Error ? "show" : ""}`}>{Error}</p>
        </form>
        </div>
  )
}

export default Login