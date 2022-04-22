import * as React from "react";

export class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            email: '',
            password: ''        
        }
        this.login = this.login.bind(this)
        this.changeUserName = this.changeUserName.bind(this);
        this.changePassword = this.changePassword.bind(this);
        this.goToPage = this.goToPage.bind(this);
        this.setAdmin = this.setAdmin.bind(this);
        this.setLoggedIn = this.setLoggedIn.bind(this);

    }
    login = (e) => {
        let data = JSON.stringify(this.state);
        const fetchUrl = this.props.myApiUrl + '/Users/SignIn';       
        fetch(fetchUrl , {
            method: 'post',
            headers: {
                'accept': '*/*',
                'Content-Type': 'application/json'
            },
            body: data
        }).then(res => {            
            if (res.status === 200){
                res.json().then(x => {
                    localStorage.setItem('jwtbearer', x.jwt);
                    localStorage.setItem('userId', x.userId);                    
                    this.setAdmin(x.isAdmin);
                    this.setLoggedIn();
                    this.goToPage(2);                    
                });
            }
            else alert("Felaktig inloggning");
        });
         e.preventDefault();
    }
    setAdmin(isAdmin){
        this.props.onAdminSet(isAdmin);
    }
    setLoggedIn(){
        this.props.onLogIn();
    }
    changeUserName(event) {
        this.setState({
            email: event.target.value
        })
    }
    changePassword(event) {
        this.setState({
            password: event.target.value
        })
    }
    goToPage(x) {
        this.props.onGoToPage(x);
    }
    render() {
        return (
            <div>
                <form autoComplete="off" onSubmit={this.login}>
                    <h3>Logga in</h3>                    
                    <div className="mb-3">
                        <label className="form-label">Användarnamn</label>
                        <input required type="text" className="form-control" value={this.state.Email} onChange={this.changeUserName} />
                        <label className="mb3">Lösenord</label>
                        <input required type="text" className="form-control" value={this.state.password} onChange={this.changePassword} />
                    </div>
                    <button type="submit" className="btn btn-primary">Logga in</button>
                </form>
                <br />
                <button className="btn btn-warning" onClick={() => this.goToPage(4)}>Skapa användare</button>
            </div>

        );
    }
}