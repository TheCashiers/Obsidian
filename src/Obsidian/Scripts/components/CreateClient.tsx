import * as React from "react"
export const CreateClient = (props) => 
<div className="well bs-component col-md-6">
    <form className="form-horizontal">
        <fieldset>
            <legend>Legend</legend>
            <div className="form-group">
                <label for="inputEmail" className="col-md-2 control-label">Email</label>

                <div className="col-md-10">
                    <input type="email" className="form-control" id="inputEmail" placeholder="Email"/>
                </div>
            </div>

            <div className="form-group">
                <label for="inputPassword" className="col-md-2 control-label">Password</label>

                <div className="col-md-10">
                    <input type="password" className="form-control" id="inputPassword" placeholder="Password"/>
                </div>
            </div>
        
            <div className="form-group">
                <div className="col-md-10 col-md-offset-2">
                    <button type="button" className="btn btn-default">Cancel</button>
                    <button type="submit" className="btn btn-primary">Submit</button>
                </div>
            </div>
        </fieldset>
    </form>
</div>