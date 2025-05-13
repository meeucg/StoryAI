<template>
    <div class="button-primary" @click="onClick">
        {{ text }}
    </div>
</template>

<script>
    export default {
        name: "ButtonPrimary",
        props: {
            text: {
                type: String,
                required: true
            },
            disabled: {
                type: Boolean,
                default: false
            }
        },
        emits: ['click'],
        setup(props, { emit }) {
            const onClick = (event) => {
                if (!props.disabled) {
                    emit('click', event)
                }
            }

            return {
                onClick
            }
        }
    }
</script>

<style>
    .button-primary {
        font-size: 13px;
        font-weight: 200;
        color: var(--text-primary-col);
        background-color: var(--tetriary-dark-col);
        padding: 0.6rem 1.2rem;
        border-radius: 18px;
        transition: all 0.3s ease;
        user-select: none;
    }

    .button-primary:not([disabled]):hover {
        cursor: pointer;
        background-color: var(--tetriary-light-col);
        padding: 0.6rem 1.5rem;
    }

    .button-primary[disabled] {
        opacity: 0.5;
        cursor: default;
    }
</style>
