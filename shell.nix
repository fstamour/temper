{ pkgs ? import <nixpkgs> {} }:
pkgs.mkShell {
  buildInputs = with pkgs; [
    pkgsCross.avr.buildPackages.gcc
    gforth
    gnumake
  ];
}
