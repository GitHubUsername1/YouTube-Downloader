   M S B A M L   `   `   `  ����  WPF YouTube Downloader%    WPF_YouTube_Downloader.Download   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        N� Title$  Downloading��5      .��5
      � ��5   
   �� 5       �� ImageSource$ dog.ico��6   .+�5   
   �� -   5        $��	lblStatus��5   m   $��Center=�6   $��36��64   $
��Left=�6@   $��0,51,0,0q�6[   $	��Top=�6~      headerTextStyle#��[  6�   $	��605��6�   � 5   
   -   5        $��progressBar1��5   Y   
���/���$��31��6/   $��Stretch=�6;   $��Bottom=�6m    � Orientation$ 
Horizontal=�6�   $��12,0,12,202q�6�   �� 5   
   -   5        $��	btnCancel��5   ^   $��Cancel��6   $��23��6"   $
��Left=�6.   $��268,284,0,0q�6I   $	��Top=�6o   $��75��6�   $y�Visible=�6�   �� 5   
   -   5        $��btnOK��5   Z   $��OK��6   $��23��6   $
��Left=�6*   $��268,284,0,0q�6E   $	��Top=�6g   $��75��6   $y�Hidden=�6�   6�   �� 5   
   -   5        $��lblPercentage��5   J   $��25��6   $
��Left=�6   $��	12,93,0,0q�67   $	��Top=�6_   $	��581��6w     DownloadLabelStyle#��[ 6�    J� RenderTransformOrigin$ 0.504,-0.49&�6�   �� 5   
   -   5        $��lblData��5   �   $��Center=�6   $��28��64   $
��Left=�6@   $��	0,0,0,148q�6[   $��Bottom=�6n   $	��605��6�   #��[  6�   $y�Visible=�6�   �� 5   
   -   5        $��
textBlock1��5   P   $��23��6   $
��Left=�6    $��546,297,0,0q�6;   $�� ��6b   $	��Top=�6j   $y�Hidden=�6�   �� 5   
   -   5        .�5   ,   
Log File66   5      o� 5   
   -	   5        $��menu1��5   O   $��23��6%   $��Stretch=�61   $	��Top=�6\   $��2��6t   ��6   Z� .@�5      m� $��_File��6-   ��6   Z� .@�5      �� m� 5      -
   5        $��mnuExit��5      $��_Exit��6*   5      5      m� 5      -   5        $��mnuAbout��5      $��_About��6'   5      5       �� 5!   
   -   5        $��
textBlock2��5!   P   $��23��6   $
��Left=�6    $��247,179,0,0q�6;   $	��Top=�6b   $y�Hidden=�6z   �� 5"      -   5         �Name$ hypOpenSaveLoc��5"   5   .�6K   View Downloaded Files6b   5#      5$      5%      