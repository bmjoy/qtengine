<div align="center">
<h1 align="center">QTEngine</h1>
<img align="center" src="https://qtlamkas.why-am-i-he.re/8pAn9B.png" />
</div>
<br />

<div align="center">
<h3>Unity Networking Library in C#</h3>
</div>

<div>
<b>Features support:</b>
</div>
[x] Multiple rooms using multiple server workers
<br />
[x] Custom components and client/server separated logic
<br />
[x] Easily synchronizable just by adding attributes
<br />
[x] Very low packet sizes (almost same as source objects) using protobuf-net allowing thousand of updates per second 
<br />
[x] Authoritive due to the low ammount of data controlled by the client
<br />
[-] Web interface to interact with the server
<br />
[x] Easy to use database using Redis
<br />
[-] Uploading/Downloading assets from the server
<br />
[-] Caching assets on a local disk
<br />
[-] Custom ticking system for scheduling events
<br />
[-] SSL Encryption
<br />
<div>
<br />
<b>Syncing progress:</b>
<div>
[x] Syncing primitives (int, float, bool, string, ...)
<br />
[x] Syncing arrays
<br />
[x] Syncing dictionaries
<br />
[x] Syncing spawning objects
<br />
[x] Syncing despawning objects
<br />
[x] Syncing position/rotation (with linear interpolation)
<br />
[-] Syncing scale
<br />
[-] Syncing client mouse buttons (configurable buttons)
<br />
[x] Syncing client input (configurable keys)
<br />
[x] Syncing client axis (configurable axises)
<br />
[x] Syncing client VR trackers position
<br />
[x] Syncing client VR trackers rotation
<br />
[x] Syncing client VR input
<br />
[-] Syncing client VR specifications
<br />
[-] Syncing client hardware specifications
<br />
[x] Syncing function calls
<br />
[x] Syncing function calls with arguments
<br />
[x] Asynchronous messare responses
<br />
[x] Syncing object ownership
<br />
[x] Syncing object active state
<br />
[-] Syncing client logs
</div>
<br />
<b>Syncing components:</b>
<div>
[x] SyncGameobject - synchronizes position and rotation of object
<br />
[x] SyncAnimation - synchronizes animation parameters of object
<br />
[-] SyncAudio - synchronizes volume, play states of object
<br />
[-] SyncVideo - synchronizes volume, play states of object
<br />
</div>
<br />
<b>Master server workflow:</b>
<div>
> Master server sets up a TCP server on port 8111
<br />
> Master server spawns server workers
<br />
</div>
<br />
<b>Server worker workflow:</b>
<div>
> Server worker sets up a TCP server on port 8112++
<br />
> Server worker connects to a master server through port 8111
<br />
> Server worker sends WorkerReadyMessage and recieves RoomInfoMessage in exchange
<br />
> Server worker sends WorkerInfoMessage to server
<br />
</div>
<br />
<b>Client workflow:</b>
<div>
> Client connects to a master server through port 8111
<br />
> Client sends RequestRoomsMessage and recieves RoomsMessage in exchange
<br />
> Client connects to a specific server worker through port specified in RoomInfo
<br />
> Client sends ClientInfoMessage and RequestSyncMessage and recieves updates to catch up with current state of the scene
<br />
> Client sends ReadyMessage to start recieving updates
<br />
</div>
</div>
