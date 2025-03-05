import socket
import threading
import csv
import time

BROADCAST_PORT = 9001
DATA_PORT = 9000

CSV_FILE = f"Data/{time.strftime('%Y-%m-%d_%H-%M-%S')}.csv"

def get_local_ip():
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.connect(("8.8.8.8", 80))
    ip = s.getsockname()[0]
    s.close()
    return ip

SERVER_IP = get_local_ip()

def broadcast_ip():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    
    while True:
        message = f"SERVER_IP:{SERVER_IP}".encode()
        sock.sendto(message, ("<broadcast>", BROADCAST_PORT))
        time.sleep(5)

def receive_data():
    sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    sock.bind((SERVER_IP, DATA_PORT))

    with open(CSV_FILE, mode='a', newline='') as file:
        writer = csv.writer(file)
        writer.writerow(["Timestamp", "X", "Y", "Z"])  # Write header

        while True:
            data, addr = sock.recvfrom(1024)
            decoded_data = data.decode()
            print(f"Received: {decoded_data} from {addr}")
            writer.writerow(decoded_data.split(","))

# Start server threads
threading.Thread(target=broadcast_ip, daemon=True).start()
threading.Thread(target=receive_data, daemon=True).start()

# Keep script running
while True:
    time.sleep(1)
