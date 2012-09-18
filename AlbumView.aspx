<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumView.aspx.cs" Inherits="PhotoData.AlbumView" %>
<asp:accessdatasource runat="server" DataFile="~/App_Data/photos.mdb" ID="AlbumListGenerator"/>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/jscript">
        function donavigate(url) {
            document.location = url;
        }

        function Face(n, haverect, t, l, w, h) {
            this.Name = n;
            this.HaveRect = haverect;
            this.Top = t;
            this.Left = l;
            this.Width = w;
            this.Height = h;
        }
        
        function Photo(url, title) {
            this.Url = url;
            this.Title = title;
            this.Faces = null;
        }

        function Photo(url, title, faces) {
            this.Url = url;
            this.Title = title;
            this.Faces = faces;
        }
        
        var photos = new Array();
        var index = 0;
        var theimage_;

        function GrabWidthAndApply() {
            theo = theimage_;
            if (setmywidth.width < theo.offsetWidth) {
                setmywidth.width = theo.offsetWidth;
            }
            if (setmywidth.height < theo.offsetHeight) {
                setmywidth.height = theo.offsetHeight;
            }
        }

        function Next() {
            GrabWidthAndApply();
            index++;
            index = index % photos.length;
            ShowIndex();
        }

        function Prev() {
            GrabWidthAndApply();
            index--;
            if (index < 0) {
                index = photos.length - 1;
            }
            ShowIndex();
        }

        function NextOrPrev() {
            var theo = theimage_;
            if (event.offsetX < (theo.offsetWidth)/2) {
                Prev();
            }
            else {
                Next();
            }
        }
        
        function ShowIndex() {
            var phnext_ = document.getElementById("phnext");
            var phprev_ = document.getElementById("phprev");

            theimage_.src = photos[index].Url;
            themessage.innerHTML = photos[index].Title;

            var indexplusone = index + 1;
            counthere.innerHTML = indexplusone + " / " + photos.length;
            var inext = (index + 1) % photos.length;
            var iprev = (index - 1);
            if (iprev < 0) iprev = photos.length - 1;
            phnext_.src = photos[inext].Url;
            phprev_.src = photos[iprev].Url;
            HandleResize();
        }

        var Showing = false;
        function ToggleShowNames() {
            var namesdiv_ = document.getElementById("nameshere");
            var namestringsroot = document.getElementById("namestrings");
            if (Showing) {
                namesdiv_.style.display = "none";
                namestringsroot.style.display = "none";
                window.event.srcElement.innerHTML = "Show Names";
            }
            else {
                namesdiv_.style.display = "";
                namestringsroot.style.display = "";
                window.event.srcElement.innerHTML = "Hide Names";
            }
            Showing = !Showing;
            HandleResize();
        }

        function HandleResize() {
            if (Showing) {
                var ow = theimage_.clientWidth;
                var oh = theimage_.clientHeight;
                
                var ww = ow / 0xffff;
                var hh = oh / 0xffff;

                var namesroot = document.getElementById("nameshere");
                namesroot.innerHTML = "";
                var namestringsroot = document.getElementById("namestrings");
                namestringsroot.innerHTML = "";
                var addedfirststring = false;
                var addedfirstname = false;
                
                for (var i = 0; i < photos.length; i++) {
                    if (i == index && photos[i].Faces != null) {
                        for (var j = 0; j < photos[i].Faces.length; j++) {
                            var face = photos[i].Faces[j];

                            if (face.HaveRect == true) {
                                var ft = face.Top * hh;
                                var fl = face.Left * ww + parseInt(theimage_.offsetLeft);
                                var fw = face.Width * ww;
                                var fh = face.Height * hh;

                                // Create the black rectangle first
                                var div1 = document.createElement("div");
                                div1.style.width = fw + "px";
                                div1.style.height = fh + "px";
                                div1.style.top = ft + "px";
                                div1.style.left = fl + "px";
                                div1.style.border = "1px solid black";
                                div1.style.borderRadius = "4px";
                                div1.style.position = "absolute";
                                namesroot.appendChild(div1);

                                // Create the white rectangle next, offset by one pixel
                                var div2 = document.createElement("div");
                                div2.style.width = fw + "px";
                                div2.style.height = fh + "px";
                                div2.style.top = (ft + 1) + "px";
                                div2.style.left = (fl + 1) + "px";
                                div2.style.border = "1px solid white";
                                div2.style.borderRadius = "4px";
                                div2.style.position = "absolute";
                                namesroot.appendChild(div2);

                                // Next create the background rectangle for the text
                                var div3 = document.createElement("div");
                                var div3top = ft + fh + 4;
                                var div3left = fl;
                                div3.style.top = div3top + "px";
                                div3.style.left = div3left + "px";
                                div3.style.color = "white";
                                div3.style.background = "white";
                                div3.style.opacity = "0.7";
                                div3.style.padding = "2px";
                                div3.style.borderRadius = "4px";
                                div3.style.position = "absolute";
                                div3.innerHTML = face.Name;
                                namesroot.appendChild(div3);

                                // Next create the white shadow text
                                var div4 = document.createElement("div");
                                div4.style.top = div3top + "px";
                                div4.style.left = div3left + "px";
                                div4.style.color = "white";
                                div4.style.padding = "2px";
                                div4.style.position = "absolute";
                                div4.innerHTML = face.Name;
                                namesroot.appendChild(div4);

                                // Finally, create the black shadow text
                                var div5 = document.createElement("div");
                                div5.style.top = (div3top + 1) + "px";
                                div5.style.left = (div3left + 1) + "px";
                                div5.style.color = "black";
                                div5.style.padding = "2px";
                                div5.style.position = "absolute";
                                div5.innerHTML = face.Name;
                                namesroot.appendChild(div5);
                            }
                            else {
                                if (addedfirststring == false) {
                                    namestringsroot.innerHTML = "People: ";
                                    addedfirststring = true;
                                }
                                var span1 = document.createElement("span");

                                if (addedfirstname) {
                                    span1.innerHTML = ", " + face.Name;
                                }
                                else {
                                    span1.innerHTML = face.Name;
                                    addedfirstname = true;
                                }
                                namestringsroot.appendChild(span1);
                            }
                        }
                    }
                }
            }
        }

        function StartThingsOff() {
            window.onresize = HandleResize;
            theimage_ = document.getElementById("theimage");
            BuildPhotoList();
        }
        
    </script>
</head>
<body id="thebody" style="background:black; color:White; font-family:Segoe UI Light"  onload="StartThingsOff()">
    <center><span style="font-size:40pt; cursor:pointer" onclick="donavigate('years.aspx')">Parikh Family Photos</span>
                   <span style="font-size:xx-small; cursor:pointer" onclick="donavigate('lucky.aspx')">I'm feeling Lucky</span></center>
    <%PutPhotoArray(); %>
    <form id="form1" runat="server">
    
<center style="margin-top:40px">

<img id="phprev" style="position:absolute; left:-10000; visibility:hidden" />
<img id="phnext" style="position:absolute; left:-10000; visibility:hidden" />
    
<table>

 <tr>
    <td valign="middle">
        <span onclick="Prev()" style="color:#585858; cursor:pointer; font-size:100pt">< </span>
    </td>
    <td>
        <table>
        <tr>
            <td align="right"><span id="counthere"> </span></td>
        </tr>
        <tr>
        <td valign="middle" align="center" id="setmywidth">
            <div style="position:relative; font-size:10pt; font-family:Segoe UI;">
                <span id="nameshere" style="display:none">
                </span>
                <img onload="HandleResize()" style="cursor:pointer; padding:0; margin:0" onclick="NextOrPrev()" id="theimage"/>
            </div>
        </td>
        </tr>
        <tr>
        <td align="center">
            <span id="themessage"></span><br/>
            <span id="namestrings"></span>
            </td>
        </tr>
        </table>
    </td>
    <td valign="middle">
        <span onclick="Next()" style="color:#585858; cursor:pointer; font-size:100pt">> </span>
    </td>
</tr>
</table>
</center>
</form>
</body>
</html>
