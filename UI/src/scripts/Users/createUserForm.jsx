import * as React from "react";
import * as ReactDOM from "react-dom";

export class CreateUserForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            email: '',
            password: '',
            password2: '',
            firstName: '',
            lastName: '',
            address: '',
            city: '',
            postalCode: '',
            isAdmin: false
        }
        this.changeIsAdmin = this.changeIsAdmin.bind(this);
        this.createUser = this.createUser.bind(this);
        this.goToPage = this.goToPage.bind(this);
    }
    changeEmail = (event) => this.setState({ email: event.target.value })
    changePassword = (event) => this.setState({ password: event.target.value })
    changePassword2 = (event) => this.setState({ password2: event.target.value })
    changeFirstName = (event) => this.setState({ firstName: event.target.value })
    changeLastName = (event) => this.setState({ lastName: event.target.value })
    changeAddress = (event) => this.setState({ address: event.target.value })
    changeCity = (event) => this.setState({ city: event.target.value })
    changePostalCode = (event) => this.setState({ postalCode: event.target.value })
    changeIsAdmin() {
        this.setState(prev => ({
            isAdmin: prev.isAdmin ? false : true
        }))
    }
    createUser(e) {
        if (this.state.password !== this.state.password2) {
            alert("Lösenorden matchar inte.");
            e.preventDefault();
            return;
        }
        let data = JSON.stringify(this.state);
        const fetchUrl = this.props.myApiUrl + '/Users/SignUp';        
        fetch(fetchUrl, {
            method: 'post',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json'
            },
            body: data
        }).then(res => {
            if (res.status === 200) {
                alert(`Välkommen som kund hos oss ${this.state.firstName}!`)
                this.goToPage();
            }
            else {
                alert("Något oväntat hände. Våra tekniker kommer jobba dygnet runt för att lösa problemet.");
            }
        });
        e.preventDefault();
        
    }
    goToPage(){
        this.props.onGoToPage(1);
    }
    render() {
        return (
            <div>
                <form autoComplete="off" onSubmit={this.createUser}>
                    <h3>Skapa ny användare</h3>
                    <div className="mb-3">
                        <label className="form-label">Email(användarnamn)</label>
                        <input required className="form-control col-4" type="text" value={this.state.email} onChange={this.changeEmail} />
                        <label className="form-label">Lösenord</label>
                        <input required className="form-control col-4" type="password" value={this.state.password} onChange={this.changePassword} />
                        <label className="form-label">Upprepa lösenord</label>
                        <input required className="form-control col-4" type="password" value={this.state.password2} onChange={this.changePassword2} />
                        <label className="form-label">Förnamn</label>
                        <input required className="form-control col-4" type="text" value={this.state.firstName} onChange={this.changeFirstName} />
                        <label className="form-label">Efternamn</label>
                        <input required className="form-control col-4" type="text" value={this.state.lastName} onChange={this.changeLastName} />
                        <label className="form-label">Adress</label>
                        <input required className="form-control col-4" type="text" value={this.state.address} onChange={this.changeAddress} />
                        <label className="form-label">Stad</label>
                        <input required className="form-control col-4" type="text" value={this.state.city} onChange={this.changeCity} />
                        <label className="form-label">Postnummer</label>
                        <input required className="form-control col-4" type="text" value={this.state.postalCode} onChange={this.changePostalCode} />
                        <label htmlFor="admin" className="form-label">Vill vara admin</label>
                        <input id="admin" type="checkbox" checked={this.state.isAdmin} onChange={this.changeIsAdmin} />
                    </div>
                    <input type="submit" className="btn btn-primary" value="Skapa användare" />
                </form>
            </div>
        );
    }
}