

import { useContext, useEffect, useRef } from 'react';
import { ThemeContext } from '../App';

export const LoginWithContext = () => {
  const emailRef = useRef();
  const passwordRef = useRef();

  const blackButtonStyle = {
    backgroundColor: '#000',
    color: '#fff',
    padding: '8px 16px',
    borderRadius: '4px',
    border: 'none',
    cursor: 'pointer',
    fontSize: '16px',
    fontWeight: 'bold',
  };

  const whiteButtonStyle = {
    backgroundColor: '#fff',
    color: '#000',
    padding: '8px 16px',
    borderRadius: '4px',
    border: '1px solid #000',
    cursor: 'pointer',
    fontSize: '16px',
    fontWeight: 'bold',
  };

  const theme = useContext(ThemeContext);

  useEffect(() => {
    console.log(theme);
  })

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Email:', emailRef.current.value);
    console.log('Password:', passwordRef.current.value);
  };

  return (
    <form onSubmit={handleSubmit}>
      <input type="email" ref={emailRef} placeholder="Email" required />
      <input type="password" ref={passwordRef} placeholder="Password" required />

      {
        theme == "light" ?
          <button style={whiteButtonStyle} type="submit">Log In</button>
          :
          <button style={blackButtonStyle} className='submit-btn' type="submit">Log In</button>
      }
    </form>
  );
};
