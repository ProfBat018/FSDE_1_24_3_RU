current_branch=$(git rev-parse --abbrev-ref HEAD)

if [[ -n $(git status --porcelain) ]]; then
    echo "Обнаружены изменения. Выполняется push в ветку '$current_branch'..."

    git add .

    commit_message="Auto-commit: $(date '+%Y-%m-%d %H:%M:%S')"
    git commit -m "$commit_message"

    git push origin "$current_branch"

    echo "✅ Успешно отправлено на GitHub (ветка: $current_branch)"
else
    echo "🟢 Нет изменений для коммита"
fi
