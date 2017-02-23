import * as $ from "jquery"
import * as React from "react";
import { Portal } from "../components/Portal"
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
    constructor(props) {
        super(props);
        this.state={ token: "" };
    }
    public componentWillMount(){
        this.state.token = this.props.location.query.access_token;
        if(this.state.token) 
            window.history.pushState(null, null, "?authorized");
    }
    public componentDidUpdate() {
        fixLayout();
    }

    public render() {
        return (
                <Portal token={this.state.token}>
                    {this.props.children}
                </Portal>
        );
    }
}