<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImagesGallery.aspx.cs"
    Inherits="DesktopModules.Web.KPWModules.ImagesGallery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
           <input type="button" id="btnOK" runat="server" value="查询" />
        </div>
        <div id="container">
            <!-- Start Advanced Gallery Html Containers -->
            <div class="navigation-container">
                <div id="thumbs" class="navigation">
                    <a class="pageLink prev" style="visibility: hidden;" href="#" title="前一页">
                    </a>
                    <ul class="thumbs noscript">
                        <li><a class="thumb" name="leaf" href="<%=Url %>astronomy/images/2538183196_8baf9a8015.jpg"
                            title="Title #0">
                            <img src="<%=Url %>astronomy/images_small/2538183196_8baf9a8015_s.jpg" alt="Title #0" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #0</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/3261/2538183196_8baf9a8015_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" name="drop" href="<%=Url %>astronomy/images/2538171134_2f77bc00d9.jpg"
                            title="Title #1">
                            <img src="<%=Url %>astronomy/images_small/2538171134_2f77bc00d9_s.jpg" alt="Title #1" />
                        </a>
                            <div class="caption">
                                Any html can be placed here ...
                            </div>
                        </li>
                        <li><a class="thumb" name="bigleaf" href="<%=Url %>astronomy/images/2538168854_f75e408156.jpg"
                            title="Title #2">
                            <img src="<%=Url %>astronomy/images_small/2538168854_f75e408156_s.jpg" alt="Title #2" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #2</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2093/2538168854_f75e408156_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" name="lizard" href="<%=Url %>astronomy/images/2538167690_c812461b7b.jpg"
                            title="Title #3">
                            <img src="<%=Url %>astronomy/images_small/2538167690_c812461b7b_s.jpg" alt="Title #3" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #3</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/3153/2538167690_c812461b7b_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2538167224_0a6075dd18.jpg"
                            title="Title #4">
                            <img src="<%=Url %>astronomy/images_small/2538167224_0a6075dd18_s.jpg" alt="Title #4" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #4</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/3150/2538167224_0a6075dd18_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2537348699_bfd38bd9fd.jpg"
                            title="Title #5">
                            <img src="<%=Url %>astronomy/images_small/2537348699_bfd38bd9fd_s.jpg" alt="Title #5" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #5</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/3204/2537348699_bfd38bd9fd_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2538164582_b9d18f9d1b.jpg"
                            title="Title #6">
                            <img src="<%=Url %>astronomy/images_small/2538164582_b9d18f9d1b_s.jpg" alt="Title #6" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #6</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/3124/2538164582_b9d18f9d1b_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2538164270_4369bbdd23.jpg"
                            title="Title #7">
                            <img src="<%=Url %>astronomy/images_small/2538164270_4369bbdd23_s.jpg" alt="Title #7" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #7</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/3205/2538164270_c7d1646ecf_o.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2538163540_c2026243d2.jpg"
                            title="Title #8">
                            <img src="<%=Url %>astronomy/images_small/2538163540_c2026243d2_s.jpg" alt="Title #8" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #8</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/3211/2538163540_c2026243d2_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2537343449_f933be8036.jpg"
                            title="Title #9">
                            <img src="<%=Url %>astronomy/images_small/2537343449_f933be8036_s.jpg" alt="Title #9" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #9</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2315/2537343449_f933be8036_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2082738157_436d1eb280.jpg"
                            title="Title #10">
                            <img src="<%=Url %>astronomy/images_small/2082738157_436d1eb280_s.jpg" alt="Title #10" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #10</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2167/2082738157_436d1eb280_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2083508720_fa906f685e.jpg"
                            title="Title #11">
                            <img src="<%=Url %>astronomy/images_small/2083508720_fa906f685e_s.jpg" alt="Title #11" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #11</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2342/2083508720_fa906f685e_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2082721339_4b06f6abba.jpg"
                            title="Title #12">
                            <img src="<%=Url %>astronomy/images_small/2082721339_4b06f6abba_s.jpg" alt="Title #12" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #12</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2132/2082721339_4b06f6abba_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2083503622_5b17f16a60.jpg"
                            title="Title #13">
                            <img src="<%=Url %>astronomy/images_small/2083503622_5b17f16a60_s.jpg" alt="Title #13" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #13</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2139/2083503622_5b17f16a60_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2083498578_114e117aab.jpg"
                            title="Title #14">
                            <img src="<%=Url %>astronomy/images_small/2083498578_114e117aab_s.jpg" alt="Title #14" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #14</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2041/2083498578_114e117aab_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2082705341_afcdda0663.jpg"
                            title="Title #15">
                            <img src="<%=Url %>astronomy/images_small/2082705341_afcdda0663_s.jpg" alt="Title #15" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #15</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2149/2082705341_afcdda0663_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2083478274_26775114dc.jpg"
                            title="Title #16">
                            <img src="<%=Url %>astronomy/images_small/2083478274_26775114dc_s.jpg" alt="Title #16" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #16</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2014/2083478274_26775114dc_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2083464534_122e849241.jpg"
                            title="Title #17">
                            <img src="<%=Url %>astronomy/images_small/2083464534_122e849241_s.jpg" alt="Title #17" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #17</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2194/2083464534_122e849241_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2538173236_b704e7622e.jpg"
                            title="Title #18">
                            <img src="<%=Url %>astronomy/images_small/2538173236_b704e7622e_s.jpg" alt="Title #18" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #18</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/3127/2538173236_b704e7622e_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2538172432_3343a47341.jpg"
                            title="Title #19">
                            <img src="<%=Url %>astronomy/images_small/2538172432_3343a47341_s.jpg" alt="Title #19" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #19</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2375/2538172432_3343a47341_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/2083476642_d00372b96f.jpg"
                            title="Title #20">
                            <img src="<%=Url %>astronomy/images_small/2083476642_d00372b96f_s.jpg" alt="Title #20" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #20</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2353/2083476642_d00372b96f_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/1502907190_7b4a2a0e34.jpg"
                            title="Title #21">
                            <img src="<%=Url %>astronomy/images_small/1502907190_7b4a2a0e34_s.jpg" alt="Title #21" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #21</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/2201/1502907190_7b4a2a0e34_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/1380178473_fc640e097a.jpg"
                            title="Title #22">
                            <img src="<%=Url %>astronomy/images_small/1380178473_fc640e097a_s.jpg" alt="Title #22" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #22</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/1116/1380178473_fc640e097a_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                        <li><a class="thumb" href="<%=Url %>astronomy/images/930424599_e75865c0d6.jpg"
                            title="Title #23">
                            <img src="<%=Url %>astronomy/images_small/930424599_e75865c0d6_s.jpg" alt="Title #23" />
                        </a>
                            <div class="caption">
                                <div class="image-title">
                                    Title #23</div>
                                <div class="image-desc">
                                    图片信息</div>
                                <div class="download">
                                    <a href="<%=Url %>astronomy/1260/930424599_e75865c0d6_b.jpg">下载JPG文件</a>
                                </div>
                            </div>
                        </li>
                    </ul>
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
