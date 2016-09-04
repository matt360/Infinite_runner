#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>
#include <assert.h>
#include <exception>


#include "codegen/il2cpp-codegen.h"


struct t140833001;
struct t140833001_marshaled;

extern "C" void t140833001_marshal(const t140833001& unmarshaled, t140833001_marshaled& marshaled);
extern "C" void t140833001_marshal_back(const t140833001_marshaled& marshaled, t140833001& unmarshaled);
extern "C" void t140833001_marshal_cleanup(t140833001_marshaled& marshaled);
