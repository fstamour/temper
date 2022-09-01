include utils.fs
include glossary.fs
include output.fs
include mode.fs
include instr-macro.fs
include instructions.fs

: test-disassembly
  +compile-mode
  10 0assembly
  ret, clc,
  #7 #12 adc,
  decompile
  free-assembly ;

.instructions
test-disassembly