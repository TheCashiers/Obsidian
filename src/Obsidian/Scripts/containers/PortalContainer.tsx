import * as $ from "jquery"
import * as React from "react";
import { Portal } from "../components/Portal"
import { NotificationCenterContainer } from "./NotificationCenterContainer";
import { PortalHeader } from "../components/PortalElements";
const fixLayout = function () {
    //Get window height and the wrapper height
    var neg = $('.main-header').outerHeight() + $('.main-footer').outerHeight();
    var window_height = $(window).height();
    var sidebar_height = $(".sidebar").height();
    //Set the min-height of the content and sidebar based on the
    //the height of the document.
    if ($("body").hasClass("fixed")) {
        $(".content-wrapper, .right-side").css('min-height', window_height - $('.main-footer').outerHeight());
    } else {
        var postSetWidth;
        if (window_height >= sidebar_height) {
            $(".content-wrapper, .right-side").css('min-height', window_height - neg);
            postSetWidth = window_height - neg;
        } else {
            $(".content-wrapper, .right-side").css('min-height', sidebar_height);
            postSetWidth = sidebar_height;
        }
        //Fix for the control sidebar height
        var controlSidebar = $(".control-sidebar");
        if (typeof controlSidebar !== "undefined") {
            if (controlSidebar.height() > postSetWidth)
                $(".content-wrapper, .right-side").css('min-height', controlSidebar.height());
        }
    }
};

export class PortalContainer extends React.Component<any, any>{
    _push: Function;
    constructor(props) {
        super(props);
        this.state = { token: "", notifications: [] as Notification[] };
        this._push = () => { };
    }
    refs: {
        [string: string]: any;
        stepInput: any;
    }
    public componentWillMount(){
        this.state.token = this.props.location.query.access_token;
        if(this.state.token) 
            window.history.pushState(null, null, "?authorized");
    }
    public componentDidUpdate() {
        fixLayout();
    }
    public componentDidMount() {
        this._push = this.refs.nc.pushNotification;
    }

    public render() {
        return (
            <Portal token={this.state.token}>
                <PortalHeader token={this.state.token} />
                    <NotificationCenterContainer ref="nc" />
                    {React.cloneElement(this.props.children, { token: this.state.token,push:this._push })}
            </Portal>
            
        );
    }
}
