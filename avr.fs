include utils.fs
include glossary.fs
include output.fs
include mode.fs
include i0.fs
include in.fs
include instructions.fs

: test-disassembly
  +compile-mode
  10 0assembly
  ret, clc,
  decompile
  free-assembly ;

.instructions
test-disassembly
