import * as React from "react";
import * as axios from "axios";
import * as api from "../configs/GlobalSettings";
import * as Notification from "./NotificationContainer";
import { ScopeForm } from "../components/Form"

export class ScopeCreationContainer extends React.Component<any, any>
{
    constructor(props: any) {
        super(props);
        this.state = { 
            claimTypes: "",
            scopeName:"",
            displayName:"",
            description:""
        };
        this.handleInputChange = this.handleInputChange.bind(this);
        //this.handleSelectChange = this.handleSelectChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }
    handleInputChange(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }
    handleSelectChange(e) {
        var state = this.state.claimTypes.map((d)=> {
            return {
                name: d.name,
                selected: (d.name === e ? !d.selected : d.selected)
            };
        });
        this.setState({ claimTypes: state });
    }
    handleSubmit(e) {
        e.preventDefault();
        let scopeName: string = this.state.scopeName.trim();
        let displayName: string = this.state.displayName.trim();
        let description: string = this.state.description.trim();
        let claimTypes:string[] = (this.state.claimTypes as string).split(",");
        if (scopeName&&displayName&&description&&claimTypes) {
            let jsonObject = { 
                displayName: displayName,
                scopeName:scopeName,
                description:description,
                claimTypes:claimTypes
             }
             axios
            axios.post(api.configs.createScope.request_uri,jsonObject )
                .then(()=>{
                    console.log(e);
                    Notification.Service.pushSuccess("Scope creation")
                })
                .catch((e) =>  Notification.Service.pushError("Scope creation",e));
        } else { return; }
    }
    render(){
        return <ScopeForm
            action="Create Scope"
            onInputChange={this.handleInputChange}
            onSubmit={this.handleSubmit}
            scopeName={this.state.scopeName}
            displayName={this.state.displayName}
            description={this.state.description}
            claimTypes={this.state.claimTypes}
        />
    }
}