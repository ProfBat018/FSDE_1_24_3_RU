import React, { Component } from 'react';

class Counter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            test: 'Counter Component',
            count: 0
        };
    }

    handleClick = () => {
        console.log(this.state);
        
        this.setState(prevState => ({
            count: prevState.count + 1
        }));
    };

    render() {
        return (
            <div>
                <button onClick={this.handleClick}>Click Me</button>
                <p>Count: {this.state.count}</p>
            </div>
        );
    }
}




export default Counter;