

const AuthForm = ({ mode, dispatch }) => {
    return (
      <div className="auth-container">
        <div className="auth-switch-buttons">
          <button
            className={mode === 'login' ? 'auth-button active' : 'auth-button'}
            onClick={() => dispatch({ type: 'SET_MODE', payload: 'login' })}
          >
            Login
          </button>
          <button
            className={mode === 'register' ? 'auth-button active' : 'auth-button'}
            onClick={() => dispatch({ type: 'SET_MODE', payload: 'register' })}
          >
            Register
          </button>
        </div>
  
        <form className="auth-form">
          {mode === 'register' && (
            <input
              type="text"
              placeholder="Username"
              name="username"
              className="auth-input"
            />
          )}
          <input
            type="email"
            placeholder="Email"
            name="email"
            className="auth-input"
          />
          <input
            type="password"
            placeholder="Password"
            name="password"
            className="auth-input"
          />
          <button type="submit" className="auth-submit-button">
            {mode === 'login' ? 'Login' : 'Register'}
          </button>
        </form>
      </div>
    );
  };

export default AuthForm;