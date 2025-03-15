import socket
import threading
import csv
import time
from pathlib import Path

BROADCAST_PORT = 9001
DATA_PORT = 9000

def getLocalIp():
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.connect(("8.8.8.8", 80))
    ip = s.getsockname()[0]
    s.close()
    return ip

SERVER_IP = getLocalIp()

def broadcastIp():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    
    while True:
        message = f"SERVER_IP:{SERVER_IP}".encode()
        sock.sendto(message, ("<broadcast>", BROADCAST_PORT))
        time.sleep(5)

def writeData(dataDir, data):
    filename = data[0]
    if filename == 'START': return
    data = data[1:]
    file = open(f"{dataDir}/{filename}.csv", mode='a', newline='')
    writer = csv.writer(file)
    writer.writerow(data)
    file.close() 
    

def setupFiles():
    data_dir = f"Data/{time.strftime('%Y-%m-%d_%H-%M-%S')}"
    print("Setting up files at " + data_dir)
    Path(data_dir).mkdir(parents=True, exist_ok=True)
    file = open(f"{data_dir}/modal_transform.csv", mode='w', newline='')
    writer = csv.writer(file);
    writer.writerow(["Timestamp", "Modal ID", "Position X", "Position Y", "Position Z", "Rotation X", "Rotation Y", "Rotation Z", "Rotation W", "Scale X", "Scale Y", "Scale Z", "Relative X", "Relative Y", "Relative Z", "Camera Position X", "Camera Position Y", "Camera Position Z", "Camera Look X", "Camera Look Y", "Camera Look Z"])
    file.close()

    file = open(f"{data_dir}/modal_actions.csv", mode='w', newline='')
    writer = csv.writer(file);
    writer.writerow(["Timestamp", "Modal ID", "Action"])
    file.close()

    file = open(f"{data_dir}/eagle_focus.csv", mode='w', newline='')
    writer = csv.writer(file);
    writer.writerow(["Timestamp", "Eagle ID", "Focus"])
    file.close()
    print("Files set up at " + data_dir)
    return data_dir

def receiveData():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.bind((SERVER_IP, DATA_PORT))
    dataDir = ""
    while True:
        data, addr = sock.recvfrom(1024)
        data = data.decode().split(",")
        if data[0] == 'START':
            dataDir = setupFiles()
        else:
            try:
                writeData(dataDir, data)
            except:
                pass

threading.Thread(target=broadcastIp, daemon=True).start()
threading.Thread(target=receiveData, daemon=True).start()

while True:
    time.sleep(1)
