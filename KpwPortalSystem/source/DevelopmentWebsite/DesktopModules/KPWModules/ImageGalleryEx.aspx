<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageGalleryEx.aspx.cs" Inherits="DesktopModules.Web.KPWModules.ImageGalleryEx" %>
<%@ Import Namespace="Nairc.KpwFramework.DataModel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../../css/galleriffic/basic.css" type="text/css" />
    <link rel="stylesheet" href="../../css/galleriffic/galleriffic-5.css" type="text/css" />
    <link rel="stylesheet" href="../../css/galleriffic/white.css" type="text/css" />
    <link type="text/css" href="../../css/redmond/jquery-ui-1.8.1.custom.css" rel="stylesheet" />
    
    <script type="text/javascript" src="../../javascript/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="../../JavaScript/jquery-ui-1.8.1.custom.min.js"></script>

    <script type="text/javascript" src="../../javascript/jquery.history.js"></script>

    <script type="text/javascript" src="../../javascript/jquery.galleriffic.js"></script>

    <script type="text/javascript" src="../../javascript/jquery.opacityrollover.js"></script>

    <!-- We only want the thunbnails to display when javascript is disabled -->

    <script type="text/javascript">
        document.write('<style>.noscript { display: none; }</style>');

        $(function() {
            $("#datepicker").datepicker();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div>
             选择日期: <input type="text" id="datepicker" runat="server"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click"/>
        </div>
        <div id="container">
            <!-- Start Advanced Gallery Html Containers -->
            <div class="navigation-container" >
                <div id="thumbs" class="navigation">
                    <a class="pageLink prev" style="visibility: hidden;" href="#" title="前一页">
                    </a>
                    <asp:Repeater ID="galleryRepeater" runat="server" >
				    <HeaderTemplate><ul class="thumbs noscript"></HeaderTemplate>
				    <ItemTemplate>
				        <li><a class="thumb" name="leaf" href="<%#((ImageInfo)Container.DataItem).Image %>"
                                title="<%#((ImageInfo)Container.DataItem).StarName%>">
                                <img src="<%#((ImageInfo)Container.DataItem).ThumbImage %>" alt="<%#((ImageInfo)Container.DataItem).StarName%>" />
                            </a>
                                <div class="caption" style="width:200px">
                                    <div class="image-title">
                                        星名称：
                                        <br />
                                        <%#((ImageInfo)Container.DataItem).StarName%></div>
                                    <div class="image-desc">
                                        拍照日期：
                                        <br />
                                        <%#((ImageInfo)Container.DataItem).CreatedDate.ToShortDateString() %></div>
                                    <div class="image-desc">
                                    拍照者：
                                    <br />
                                    <%#((ImageInfo)Container.DataItem).CreatedBy %></div>
                                    <div class="download">
                                        <a href='<%#((ImageInfo)Container.DataItem).BigImage %>'>下载JPG文件</a>
                                    </div> 
                                </div>

                          </li>				
				    </ItemTemplate>
				    <FooterTemplate></ul></FooterTemplate>
				    </asp:Repeater>                   
                    <a class="pageLink next" style="visibility: hidden;" href="#" title="后一页">
                    </a>
                </div>
            </div>
            <div class="content">
                <div class="slideshow-container">
                    <div id="controls" class="controls">
                    </div>
                    <div id="loading" class="loader">
                    </div>
                    <div id="slideshow" class="slideshow">
                    </div>
                </div>
                <div id="caption" class="caption-container">
                    <div class="photo-index">
                    </div>
                </div>
            </div>
            <!-- End Gallery Html Containers -->
            <div style="clear: both;">
            </div>
    </div>
    </div>
    </form>
    
    <script type="text/javascript">
        jQuery(document).ready(function($) {
            // We only want these styles applied when javascript is enabled
            $('div.content').css('display', 'block');

            // Initially set opacity on thumbs and add
            // additional styling for hover effect on thumbs
            var onMouseOutOpacity = 0.67;
            $('#thumbs ul.thumbs li, div.navigation a.pageLink').opacityrollover({
                mouseOutOpacity: onMouseOutOpacity,
                mouseOverOpacity: 1.0,
                fadeSpeed: 'fast',
                exemptionSelector: '.selected'
            });

            // Initialize Advanced Galleriffic Gallery
            var gallery = $('#thumbs').galleriffic({
                delay: 2500,
                numThumbs: 8,
                preloadAhead: 8,
                enableTopPager: false,
                enableBottomPager: false,
                imageContainerSel: '#slideshow',
                controlsContainerSel: '#controls',
                captionContainerSel: '#caption',
                loadingContainerSel: '#loading',
                renderSSControls: true,
                renderNavControls: true,
                playLinkText: '播放图像',
                pauseLinkText: '停止播放',
                prevLinkText: '&lsaquo; 前一张图片',
                nextLinkText: '后一张图片 &rsaquo;',
                nextPageLinkText: '后 &rsaquo;',
                prevPageLinkText: '&lsaquo; 前',
                enableHistory: true,
                autoStart: false,
                syncTransitions: true,
                defaultTransitionDuration: 900,
                onSlideChange: function(prevIndex, nextIndex) {
                    // 'this' refers to the gallery, which is an extension of $('#thumbs')
                    this.find('ul.thumbs').children()
							.eq(prevIndex).fadeTo('fast', onMouseOutOpacity).end()
							.eq(nextIndex).fadeTo('fast', 1.0);

                    // Update the photo index display
                    this.$captionContainer.find('div.photo-index')
							.html('图片 ' + (nextIndex + 1) + ' 总计 ' + this.data.length);
                },
                onPageTransitionOut: function(callback) {
                    this.fadeTo('fast', 0.0, callback);
                },
                onPageTransitionIn: function() {
                    var prevPageLink = this.find('a.prev').css('visibility', 'hidden');
                    var nextPageLink = this.find('a.next').css('visibility', 'hidden');

                    // Show appropriate next / prev page links
                    if (this.displayedPage > 0)
                        prevPageLink.css('visibility', 'visible');

                    var lastPage = this.getNumPages() - 1;
                    if (this.displayedPage < lastPage)
                        nextPageLink.css('visibility', 'visible');

                    this.fadeTo('fast', 1.0);
                }
            });

            /**************** Event handlers for custom next / prev page links **********************/

            gallery.find('a.prev').click(function(e) {
                gallery.previousPage();
                e.preventDefault();
            });

            gallery.find('a.next').click(function(e) {
                gallery.nextPage();
                e.preventDefault();
            });

            /****************************************************************************************/

            /**** Functions to support integration of galleriffic with the jquery.history plugin ****/

            // PageLoad function
            // This function is called when:
            // 1. after calling $.historyInit();
            // 2. after calling $.historyLoad();
            // 3. after pushing "Go Back" button of a browser
            function pageload(hash) {
                // alert("pageload: " + hash);
                // hash doesn't contain the first # character.
                if (hash) {
                    $.galleriffic.gotoImage(hash);
                } else {
                    gallery.gotoIndex(0);
                }
            }

            // Initialize history plugin.
            // The callback is called at once by present location.hash. 
            $.historyInit(pageload, "advanced.html");

            // set onlick event for buttons using the jQuery 1.3 live method
            $("a[rel='history']").live('click', function(e) {
                if (e.button != 0) return true;

                var hash = this.href;
                hash = hash.replace(/^.*#/, '');

                // moves to a new page. 
                // pageload is called at once. 
                // hash don't contain "#", "?"
                $.historyLoad(hash);

                return false;
            });

            /****************************************************************************************/
        });
    </script>
</body>
</html>
