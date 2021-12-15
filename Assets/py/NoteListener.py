import UdpComms as U
import time
import socket
from rtmidi.midiutil import open_midioutput
from rtmidi.midiconstants import NOTE_OFF, NOTE_ON, CONTROL_CHANGE, PITCH_BEND

midiout, midiportname = open_midioutput()

# Create UDP socket to use for sending (and receiving)
sock = U.UdpComms(udpIP="127.0.0.1", portTX=8000, portRX=8001, enableRX=True, suppressWarnings=True)

while True:
    data = sock.ReadReceivedData()  # read data

    if data != None:  # if NEW data has been received since last ReadReceivedData function call
        print(data)  # print new received data
        fields = data.split(" ")
        if (fields[0] == "NoteOn"):
            note = int(fields[1])
            vel = 122
            midiout.send_message([NOTE_ON, note, vel])
        elif (fields[0] == "NoteOff"):
            note = int(fields[1])
            vel = 122
            midiout.send_message([NOTE_OFF, note, vel])


    time.sleep(0.05)
