import * as React from "react";
import * as ReactDOM from "react-dom";

export class UserInfo extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            firstName: "Test",
            lastName: "Testsson",
            email: "test@test.com",
            address: "Testvägen 1",
            postalCode: "12345",
            city: "Testby",
            hideDeleteButton: true
        }
        this.showDeleteButton = this.showDeleteButton.bind(this);
        this.deleteUser = this.deleteUser.bind(this);

    }
    componentDidMount() {
        let fetchUrl = this.props.myApiUrl + '/users/' + localStorage.getItem('userId');
        fetch(fetchUrl, {
            'method': 'get',
            'headers': {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('jwtbearer')}`
            }
        }).then(res => {
            if (res.status === 200) {
                res.json().then(json => {
                    this.setState({
                        firstName: json.firstName,
                        lastName: json.lastName,
                        email: json.email,
                        address: json.address,
                        postalCode: json.postalCode,
                        city: json.city
                    })
                }
                )
            }
        })
    }
    showDeleteButton() {
        alert("Du aktiverar nu den röda knappen!");
        this.setState({
            hideDeleteButton: false
        })
    }
    deleteUser() {
        let fetchUrl = this.props.myApiUrl + '/users/' + localStorage.getItem('userId');
        fetch(fetchUrl, {
            'method': 'delete',
            'headers': {
                'accept': '*/*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem('jwtbearer')}`
            }
        }).then(res => {
            if (res.status === 200) {
                alert("Du är borttagen ur systemet och loggas nu ut!");
                localStorage.removeItem('jwtbearer');
                localStorage.removeItem('userId');
                location.reload();
            }
            else alert("Något gick fel.")
        })
    }
    render() {
        return (
            <div>
                <h3>Användarprofil</h3>
                <h5>Användarnamn: <b>{this.state.email}</b></h5>
                <ul>
                    <li>{this.state.firstName} {this.state.lastName}</li>
                    <li>{this.state.address}</li>
                    <li>{this.state.postalCode} {this.state.city}</li>
                </ul>
                <p>
                    <button className="btn btn-warning" onClick={this.showDeleteButton}>Ta bort konto</button>
                </p>
                <p>
                    <button type="submit" className="btn btn-danger" onClick={this.deleteUser} hidden={this.state.hideDeleteButton}>Bekräfta borttagning</button>
                </p>
            </div>
        );
    }
}