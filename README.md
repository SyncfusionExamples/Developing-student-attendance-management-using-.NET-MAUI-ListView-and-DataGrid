# Developing-student-attendance-management-using-.NET-MAUI-ListView-and-DataGrid

This example demonstrates how to develop a student attendance management system using .NET MAUI ListView and DataGrid.

## Sample

```xaml
<syncfusion:SfListView
        x:Name="listView"
        FooterSize="60"
        HeaderSize="70"
        IsStickyFooter="True"
        IsStickyHeader="True"
        ItemSize="72"
        ItemsSource="{Binding Students}"
        SelectionBackground="Transparent">
        <syncfusion:SfListView.HeaderTemplate>
            <DataTemplate>
                ...
            </DataTemplate>
        </syncfusion:SfListView.HeaderTemplate>

        <syncfusion:SfListView.ItemTemplate>
            <DataTemplate x:DataType="local:StudentInfo">
                ...
            </DataTemplate>
        </syncfusion:SfListView.ItemTemplate>

        <syncfusion:SfListView.FooterTemplate>
            <DataTemplate>
                ...
            </DataTemplate>
        </syncfusion:SfListView.FooterTemplate>
    </syncfusion:SfListView>
```

```xaml
<ContentPage.Resources>
    <local:ForeColorConverter x:Key="foreColorconverter"/>
    <local:MonthConverter x:Key="monthConverter"/>
    <Style TargetType="syncfusion:DataGridCell">
        <Setter Property="TextColor" Value="{Binding Source={RelativeSource Mode=Self}, Converter={StaticResource Key=foreColorconverter}}"/>
    </Style>
</ContentPage.Resources>

<Grid RowDefinitions="Auto,*">
    <Label Text="{Binding AttendanceInfoByMonth, Converter={StaticResource monthConverter}}" Margin="10" FontSize="{OnPlatform Android=Large, WinUI=Medium, iOS=Large, MacCatalyst=Large}"/>
    <syncfusion:SfDataGrid ItemsSource="{Binding AttendanceInfoByMonth.StudentAttendanceCollection}" 
                        Loaded="SfDataGrid_Loaded"   
                        FrozenColumnCount="2"
                            Grid.Row="1"
                        AutoGeneratingColumn="SfDataGrid_AutoGeneratingColumn" >
        <syncfusion:DataGridHeaderCell>
            <syncfusion:DataGridHeaderCell FontAttributes="Bold"/>
        </syncfusion:DataGridHeaderCell>
    </syncfusion:SfDataGrid>
</Grid>
```

## Requirements to run the demo

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting

### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.
