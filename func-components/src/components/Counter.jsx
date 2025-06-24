import React, { useState, useEffect } from 'react';

export const Counter = () => {

    const [count, setCount] = useState(0);
    const [firstName, setFirstName] = useState('');
    
    useEffect(() => {
        console.log("Component Updated");
    }, [firstName]);

    function incrementCount() {
        setCount(count + 1);
        console.log(count);
    }

    const testHandler = () => {
        setFirstName('Elvin' + count);
    }

    return (
        <div>
            <h1>This is a counter</h1>
            <button onClick={incrementCount}>Click</button>
            <button onClick={testHandler}>Click</button>
            <p>{count}</p>
        </div>
    );
}
