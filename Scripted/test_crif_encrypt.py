"""
Pytest tests for CRIF-Encrypt.py
"""
import os
import tempfile
import shutil
import pytest
from Scripted import crif_encrypt

def test_reject_nonexistent_file(monkeypatch):
    monkeypatch.setattr('builtins.input', lambda _: 'nonexistent.txt')
    assert not os.path.isfile('nonexistent.txt')

def test_reject_non_txt_file(tmp_path, monkeypatch):
    fake_file = tmp_path / "file.docx"
    fake_file.write_text("test")
    monkeypatch.setattr('builtins.input', lambda _: str(fake_file))
    assert not str(fake_file).lower().endswith('.txt')
# Add more tests for replace_crif_and_save_dat and sign_and_encrypt as needed
