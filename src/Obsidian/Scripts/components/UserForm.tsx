import * as React from "react";
import { Link } from "react-router";
export const UserForm = (props) => (
    

    <div className="well bs-component col-md-6">
    <form className="form-horizontal" onSubmit={props.onSubmit}>
        <fieldset>
            <legend>{props.action}</legend>
            <div className="form-group">
                <label className="col-md-2 control-label">Username</label>

                <div className="col-md-10">
                    <input type="text" className="form-control" name="username" onChange={props.onInputChange} value={props.username} placeholder="Username..."/>
                </div>
            </div>

            <div className="form-group">
                <label className="col-md-2 control-label">Password</label>

                <div className="col-md-10">
                    <input type="password" className="form-control" name="password" onChange={props.onInputChange} value={props.password} placeholder="Password.."></input>
                </div>
            </div>
        
            <div className="form-group">
                <div className="col-md-10 col-md-offset-2">
                    <Link to="/manage/users">
                        <button type="button" className="btn btn-default">Cancel</button>
                    </Link>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </div>
            </div>
        </fieldset>
    </form>
</div>
);