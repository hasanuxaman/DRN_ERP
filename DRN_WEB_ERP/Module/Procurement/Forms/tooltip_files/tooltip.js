
    function MoveObject(mx,my,object)
    {
    
        document.getElementById(object).style.display="";
        try
        {        
                var mouseX=0;
                var mouseY=0;        
                var y=0;
                var x=0;
                
                var top_position=0;
                
                if (navigator.appName == "Microsoft Internet Explorer")
                {
                     y=document.documentElement.scrollTop ;
                     x=document.documentElement.scrollLeft ;                     
                     mouseX=event.clientX;
                     mouseY=event.clientY;                     
                     top_position=parseInt(y)+parseInt(mouseY);                     
                }
                else{
                    y=window.pageYOffset;         
                    x=window.pageXOffset;         
                    mouseX=mx;
                    mouseY=my;
                    top_position=parseInt(mouseY);
                }
                document.getElementById(object).style.position = "absolute";                
                //document.getElementById(object).style.left = '100px';     
                
                document.getElementById(object).style.top=top_position+2+'px';                
                document.getElementById(object).style.left=parseInt(mouseX)+'px';                
        }
        catch(e)
        {
            
        }
    }
    function HideObject(object)
    {
        document.getElementById(object).style.display="none";
    }