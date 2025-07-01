"""
CRIF-Encrypt Python Script
Interactive script to replicate the C# app logic
"""
import os
import sys
import time
import shutil
import zipfile

def show_intro():
    print("(c) Hayk Jomardyan 2022. All rights reserved.  v16")
    print("https://github.com/jomardyan/CRIF-Import-Creator\n")
    print("Initializing, please wait...\n")
    time.sleep(0.1)

def remove_first_and_last_lines(lines):
    if len(lines) <= 2:
        return []
    return lines[1:-1]

def replace_crif_and_save_dat(file_name, directory):
    file_base_name = os.path.splitext(os.path.basename(file_name))[0]
    with open(file_name, 'r', encoding='utf-8') as f:
        text = f.read()
    text = text.replace('\t', '^~')
    lines = text.splitlines()
    lines = remove_first_and_last_lines(lines)
    dat_dir = os.path.join(directory, file_base_name, file_base_name)
    os.makedirs(dat_dir, exist_ok=True)
    dat_output = os.path.join(dat_dir, f"{file_base_name}.dat")
    with open(dat_output, 'w', encoding='utf-8') as f:
        f.write('\n'.join(lines) + '\n')
    print(f"Saved: {dat_output}")
    time.sleep(0.5)
    zip_path = os.path.join(directory, file_base_name, f"{file_base_name}.zip")
    with zipfile.ZipFile(zip_path, 'w', zipfile.ZIP_DEFLATED) as zipf:
        for root, _, files in os.walk(dat_dir):
            for file in files:
                zipf.write(os.path.join(root, file), arcname=file)
    shutil.rmtree(dat_dir)
    print(f"ZIP PATH: {zip_path}")
    return zip_path

def sign_and_encrypt(input_directory, file_name_without_extension):
    zip_path = os.path.join(input_directory, file_name_without_extension, f"{file_name_without_extension}.zip")
    cmd = f"gpg -v -se -r CRIF-SWO-PROD --passphrase '' --local-user 0x9F674BC8 '{zip_path}'"
    print("Sign and encrypt...")
    result = os.system(cmd)
    return result == 0

def main():
    show_intro()
    file_path = input("Please enter the path to the .txt file (exported UNICODE TEXT from Excel): ")
    if not os.path.isfile(file_path):
        print("File does not exist. Exiting...")
        return
    if not file_path.lower().endswith('.txt'):
        print("Input is not a .txt file. Use the exported UNICODE TEXT file from Excel.")
        return
    with open(file_path, 'r', encoding='utf-8') as f:
        input_text = f.read()
    input_directory = os.path.dirname(file_path) + os.sep
    file_name_without_extension = os.path.splitext(os.path.basename(file_path))[0]
    file_with_extension = input_directory + file_name_without_extension + os.path.splitext(file_path)[1]
    with open(file_with_extension, 'w', encoding='utf-8') as f:
        f.write(input_text)
    replace_crif_and_save_dat(file_with_extension, input_directory)
    time.sleep(0.5)
    if not sign_and_encrypt(input_directory, file_name_without_extension):
        print("An error occurred during signing and encryption.")
        return
    print("--------------SIGN AND ENCRYPT FINISHED-------------------")
    print("---------All operations completed successfully------------")
    input("Type any key to exit...")

if __name__ == "__main__":
    main()
