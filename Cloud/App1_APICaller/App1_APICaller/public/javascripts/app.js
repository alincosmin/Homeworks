var SearchForTrack = React.createClass({
    getInitialState: function(){
        return {searched: ''};
    },

    handleClick: function(event) {
        var msg = $("#searchInput")[0].value;
        this.setState({searched: msg});
        var self = this;

        $.get("https://api.spotify.com/v1/search?type=track&q=" + msg)
            .done(function(data){
                var tracklist = data.tracks.items.map(function(track){      
                    return {artist: track.artists[0].name, name: track.name, id:track.id};
                });

                self.props.setlist(tracklist);
            })
            .fail(function(response){
                console.log(response);
            });
        
    },

    render: function() {
        return (
            <div><p><input type="text" id="searchInput" /><button id="searchButton" onClick={this.handleClick}>Click me</button></p>
            <p>You searched for {this.state.searched}:</p></div>
        );
    }
});

var TrackModel = React.createClass({
    handleClick: function(event){
        var props = this.props;
        this.props.settrack({name : props.trackname, artist: props.trackartist});
    },

    render: function(){
        return (<tr onClick={this.handleClick}><td><span>{this.props.trackartist}</span></td><td><span>{this.props.trackname}</span></td></tr>);
    }
});

var YTVideoEmed = React.createClass({
    render: function(){
        return (<p>{this.props.spotifyid}</p>);
    }
});

var TrackTable = React.createClass({
    render: function(){
        var tracks = this.props.data.map(function(track){
            return (<TrackModel trackname={track.name} trackartist={track.artist} settrack={this.props.settrack} />);
        });

        return (
                <table className="table table-hover">
                <thead>
                    <th>Artist</th>
                    <th>Name</th>
                </thead>
                <tbody>
                {tracks}
                </tbody>
                </table>
            );
    }
});

var PageModel = React.createClass({
    getInitialState: function(){
        return ({tracks: [{name: 'test', artist: 'test'}]});
    },

    setTrackList: function(list){
        this.setState({tracks: list});
    },

    giveCurrentTrack: function(track){
        alert(track.name + ' ' + track.artist);
    },

    render: function(){
        return(
                <div>
                    <SearchForTrack setlist={this.setTrackList} />
                    <TrackTable data={this.state.tracks} settrack={this.giveCurrentTrack}/>
                </div>
            );
    }
});

React.render(<PageModel />,
            document.getElementById('maincontent'));

