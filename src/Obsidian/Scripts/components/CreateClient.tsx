import * as React from "react";
import { Link } from "react-router";
export const CreateClient = (props) => 
<div className="well bs-component col-md-6">
    <form onSubmit={props.onSubmit} className="form-horizontal">
        <fieldset>
            <legend>Create client</legend>
            <div className="form-group">
                <label for="inputDisplayName" className="col-md-2 control-label">Display Name</label>

                <div className="col-md-10">
                    <input type="text" name="displayName" className="form-control" onChange={props.onInputChange} value={props.displayName} placeholder="Display Name..."/>
                </div>
            </div>

            <div className="form-group">
                <label for="inputRedirectUri" className="col-md-2 control-label">Redirect Uri</label>

                <div className="col-md-10">
                    <input type="text" name="redirectUri" className="form-control" onChange={props.onInputChange} value={props.redirectUri} placeholder="http://example.com"/>
                </div>
            </div>
        
            <div className="form-group">
                <div className="col-md-10 col-md-offset-2">
                    <Link to="/manage/clients">
                    <button type="button" className="btn btn-default">Cancel</button>
                    </Link>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </div>
            </div>
        </fieldset>
    </form>
</div>