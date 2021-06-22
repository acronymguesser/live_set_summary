# LiveSetSummary

## What is it?

The LiveSetSummary app extracts basic information from an Ableton live set file,
so you don't need to open the file to see what's inside.

it currently includes all tracks names, effects used (both in tracks and master)
and song tempo (although it does not support automated tempo track :cry:)

This may be handy if you are like me and make several copies of the project 
with printed tracks (to save some processor, because our computers are slow and
we cannot afford a faster one because we have spent all the money in a
synthesizer) and want to quickly look up the original track to make some
adjustments.

You will get something like this:

```yaml
Tracks:
- Type: Audio
  Name: 34 FX 27
  Devices:
  - Type: Plugin
    Name: OrilRiver
- Type: Audio
  Name: 38 BASS 2
  Devices:
  - Type: Plugin
    Name: PTEq-X
  - Type: Plugin
    Name: TDR Nova
- Type: Midi
  Name: 44 SYNTH 14
  Devices:
  - Type: Plugin
    Name: Synth1.Librarian.64
  - Type: Plugin
    Name: TDR Nova
  - Type: Plugin
    Name: OrilRiver
MasterTrack:
  Type: Master
  Name: Master
  Devices:
  - Type: Plugin
    Name: SN03G Tape Recorder x64
  - Type: Plugin
    Name: ISOL8 x64
Tempo: 120
```

## Usage

Drag and drop an Ableton _.als_ file on _LiveSetSummary.exe_ and get a nice YAML
file with the details.

The YAML file will be located in the same folder as the _.als_ file, with the 
same name but just different extension.

*Be careful and make copies of the YAML files if necessary.*

**Actually, MAKE COPIES OF EVERYTHING as I cannot guarantee this app will not
mess up anything.**

You can also use it from command line:

```bash
LiveSetSummary.exe "C:\MusicProjects\NiceSong1\NiceSong1.als"
```

This will generate a file `C:\MusicProjects\NiceSong1\NiceSong1.yaml`

I hope you find it useful :slightly_smiling_face: