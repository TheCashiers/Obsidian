import * as React from "react";
import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import * as Notification from "./NotificationContainer";
import { CreateScope } from "../components/CreateScope"

export class ScopeCreationContainer extends React.Component<any, any>
{
    constructor(props: any) {
        super(props);
        this.state = { claims: [
                { name: "Email", selected: false },
                { name: "Gender", selected: false },
                { name: "GivenName", selected: false },
                { name: "Role", selected: false },
                { name: "NameIdentifier", selected: false }
            ],
            scopeName:"",
            displayName:"",
            description:""
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSelectChange = this.handleSelectChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    public componentDidMount(){
        ($ as any).material.init();
    }
    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value as boolean
        });
    }
    handleSelectChange(e) {
        var state = this.state.claims.map((d)=> {
            return {
                name: d.name,
                selected: (d.name === e ? !d.selected : d.selected)
            };
        });
        this.setState({ claims: state });
    }
    handleSubmit(e) {
        e.preventDefault();
        let scopeName: string = this.state.scopeName.trim();
        let displayName: string = this.state.displayName.trim();
        let description: string = this.state.description.trim();
        let claimString:string = this.state.claims.filter((d)=>{
            return d.selected 
        }).map((d)=>d.name).toString();
        console.log(claimString);
        if (claimString) {
            axios.post(api.configs.createClient.request_uri, { 
                displayName: displayName,
                scopeName:scopeName,
                description:description,
                claims:`[${claimString}]`
             })
                .then(()=>{
                    console.log(e);
                    Notification.Service.pushSuccess("Client creation")
                })
                .catch((e) =>  Notification.Service.pushError("Client creation",e));
        } else { return; }
    }
    render(){
        return <CreateScope
            onInputChange={this.handleInputChange}
            onSelectChange={this.handleSelectChange}
            onSubmit={this.handleSubmit}
            scopeName={this.state.scopeName}
            displayName={this.state.displayName}
            description={this.state.description}
            claims={this.state.claims}
        />
    }
}