import * as React from "react";
import * as ReactDOM from "react-dom";

export class Navbar extends React.Component {
    constructor(props) {
        super(props);
       
        this.toggleComponent = this.toggleComponent.bind(this);

    }
    toggleComponent(component) {             
        this.props.showPage(component);
    }
    render() {
        return (
            <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
                <div className="container-fluid">                    
                    <button className="navbar-brand btn" onClick={() => this.toggleComponent(1)}>Inloggning</button>
                    <button disabled={!this.props.isLoggedIn} className="navbar-brand btn" onClick={() => this.toggleComponent(2)}>Produkter</button>
                    <button disabled={!this.props.isLoggedIn} className="navbar-brand btn" onClick={() => this.toggleComponent(3)}>Ordrar</button>
                    <button disabled={!this.props.isLoggedIn} className="navbar-brand btn" onClick={() => this.toggleComponent(4)}>Användare</button>
                    <button disabled={!this.props.isLoggedIn} className="navbar-brand btn" onClick={() => this.toggleComponent(5)}>Logga ut</button>
                </div>
            </nav>
        );
    }
}



