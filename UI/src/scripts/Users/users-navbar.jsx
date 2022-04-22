import * as React from "react";
import * as ReactDOM from "react-dom";


export class UsersNavbar extends React.Component {
    constructor(props) {
        super(props);      
        this.toggleComponent = this.toggleComponent.bind(this);

    }  
    
    toggleComponent(component) {
        this.props.showPage(component);
    }
    
    render() {
        return (
            <nav className="navbar navbar-default">
                <div className="container-fluid">
                    <button disabled={this.props.loggedIn} className="navbar-brand btn" onClick={() => this.toggleComponent(1)}>Skapa användare</button>
                    <button disabled={!this.props.loggedIn} className="navbar-brand btn" onClick={() => this.toggleComponent(2)}>Din profil</button>
                    <button disabled={!this.props.isAdmin} className="navbar-brand btn" onClick={() => this.toggleComponent(3)}>Admin</button>
                </div>
            </nav>
        );
    }
}