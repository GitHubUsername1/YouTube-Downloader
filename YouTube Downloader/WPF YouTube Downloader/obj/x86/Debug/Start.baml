   M S B A M L   `   `   `  ����Q  MWPF YouTube Downloader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WPF_YouTube_DownloaderWPF_YouTube_Downloader  5      "    WPF_YouTube_Downloader.Start   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8testing$clr-namespace:WPF_YouTube_Downloader     5        N� Title$  YouTube Downloader - Home��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��465* �6   �� 5      $
��140* �6   ��5   
   �� 5       �� ImageSource$ dog.ico��6   .+�5   
   o� -   5        $��menu1��5   O   $��23��6%   $��Stretch=�61   $	��Top=�6\   $��2��6t   ��6   Z� .@�5      m� $��_File��6-   ��6   Z� .@�5      m� -   5        $��
mnuOptions��5      $��_Options��6-   5      m� 5      -   5        $��mnuExtractAudio��5      $'��!_Extract Audio (.mp3) From a File��62   5      �� 5      m� 5      -   5        $��mnuExit��5      $��_Exit��6*   5      5      m� 5      -   5        $��mnuAbout��5      $��_About��6'   5      5      �� 5    
   -   5        $��btnNext��5    \   $
��Next��6   $��23��6    $��Right=�6,   $��
0,170,12,0q�6H   $	��Top=�6k   $��75��6�      buttonStyle#��[  6�   $��1��6�   �� 5!   
   -   5        $��txtLink��5!   M   $��23��6   $
��Left=�6   $��
12,112,0,0q�69   $	��Top=�6\   $	��571��6t   $��2��6�    �� 
AcceptsTab . .��6�    � ToolTip 5"      �� 5#      $~��xEnter link i.e. https://www.youtube.com/watch?v=a150&index=1 OR https://www.youtube.com/playlist?list=PL91A9759EE1E6A108��60   ��6   Z� 60   .��5$      5%      �� 5&   
   -   5        $��label1��5&   _   $��
Enter Link��6   $��28��6%   $
��Left=�61   $��	12,44,0,0q�6L   $	��Top=�6m     headerTextStyle#��[ 6�   5'      5(      